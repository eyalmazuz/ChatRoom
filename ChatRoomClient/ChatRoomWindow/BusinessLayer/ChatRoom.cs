using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomWindow.PersistentLayer;
using System.Threading;

namespace ChatRoomWindow.BusinessLayer
{
    public class ChatRoom
    {
        //Fields:
        private User CurrentUser;
        private static String _url;
        public static String url { get { return _url; } }
        MessageHandler messageHandler;

        private List<DataMessage> messageList;
        JokesHandler jokesHandler;
        private String lastDt;
          


        //Constructor:
        /*Bgu server: http://ise172.ise.bgu.ac.il:80*/
        public ChatRoom()
        {
            
            _url = "http://ise172.ise.bgu.ac.il:80";
            this.CurrentUser = null;
            this.messageList = new List<DataMessage>();
            messageHandler = new MessageHandler();
            jokesHandler = new JokesHandler();
            resetList();
            RetriveTenlastMessages();



        }

        //Methods:

        //Register user to the chatRoom
        public void Register(String nickName, String G_ID,String hashPassword)
        {


            try {
                User user = User.create(nickName, G_ID, hashPassword);
                user.Register();
                }

            
            catch(Exception e)
            {
                throw e;
            }

        }

        //Login user to the Chatroom
        public void Login(String Nickname, String G_id, String hashPassword)
        {
            try
            {

                User user = User.create(Nickname, G_id, hashPassword);
                user.Login();
                this.CurrentUser = user;
            }

            catch(Exception ex)
            
            {
                throw ex;    
            }


        }



        //Recive a message and changed its text.
        public void editMessage(DataMessage m, String editText)
        {
            try
            {
                DateTime msgDateTime = DateTime.Now.ToLocalTime();
                FuncMessage newMessage = new FuncMessage(editText, m.guid, msgDateTime, m.user);
                newMessage.replace();
            }
            catch(Exception e)
            {
                throw e;
            }

        }


        //Logut the user from the chatroom
        public void Logout()


        {
            CurrentUser = null;

        }


        //Send message to the Server
        public Boolean SendMessage(String messageBody)
        {
            if (!ExceptionHandler.CheckValidty(messageBody))
                return false;
            try
            {
                FuncMessage sentMsg = CurrentUser.sendMessage(messageBody);
                RetriveTenlastMessages();



            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }



            return true;

        }

        //Get  messages from the server
        public void RetriveTenlastMessages()
        {
            try
            {
                List<DataMessage> msgList = messageHandler.getMessageList(lastDt);

                if (msgList.Any())
                {
                    msgList = msgList.OrderBy( m => m.dateTime).ToList();
                    lastDt = createString(msgList[msgList.Count-1].dateTime.ToUniversalTime());
                    
                }
                    
                merge(msgList);
              //throw new Exception(createString(msgList[0].dateTime.ToUniversalTime()));


            }
            catch (Exception e)
            {
                throw e;
               throw new Exception("Error! Cant retrive Masseges! Server issues...");
            }


        }

        //Recive a DateTime object and returns a String that fits to the SQL format
        private string createString(DateTime dateTime)
        {
            String output = dateTime.Year+"-"+dateTime.Month+"-"+dateTime.Day+" "+dateTime.Hour+":"+dateTime.Minute+":"+dateTime.Second+"."+dateTime.Millisecond;
            return output;
        }

        //Runs on the messege List and set true on each message of the current User.
        private void updateEditable(List<DataMessage> msgList)
        {
            foreach(DataMessage m in msgList)
            {
                if (m.user.Equals(CurrentUser))
                    m.Editable = true;
            }
        }

        //Union two Lists of messages
        private void merge(List<DataMessage> toMerge)
        {

            messageList = messageList.OrderBy(m => m.dateTime).ToList();
            while (toMerge.Any())
            {
                DataMessage m = toMerge[0];
                toMerge.Remove(m);
                insert(m, messageList);
                
            }

        }
        //Add a Messege to a certein List
        private void insert(DataMessage data , List<DataMessage> usedList)
        {
            if (usedList.Count == 200)
                usedList.RemoveAt(0);
            List<DataMessage> clone = (from m in usedList where m.guid == data.guid select m).ToList();
            if (clone.Any())
                usedList.Remove(clone[0]);
            usedList.Add(data);

        }

        //Sort and filter the Message List by operating LinqActions on it.
        public List<DataMessage> FilterSortList(List<ILinqAction> actionList)
        { 

            foreach (ILinqAction action in actionList)
            {
                messageList = action.operate(messageList);
            }
            updateEditable(messageList);
            return messageList;

        }

      
        //A pipeline Between the Presentation Layer and the Communication Layer to handel filters
        public void AddGroupFilter(int Group_id)
        {
            messageHandler.AddGroupFilter(Group_id);
            resetList();
        }
        public void AddNicknameFilter(String Nickname)
        {
            messageHandler.AddNicknameFilter(Nickname);
            resetList();
        }
        public void clearFilters()
        {
            messageHandler.clearFilters();
            resetList();
        }

        //Reset the message List 
        private void resetList()
        {
            messageList.Clear();
            lastDt = null;
        }

        public String getJoke()
        {
            List<String> lines = jokesHandler.readFile();
            int count = lines.Count();
            Random rnd = new Random();
            int skip = rnd.Next(0, count);
            string line = lines.Skip(skip).First();
            return line;

        }
    }

}





