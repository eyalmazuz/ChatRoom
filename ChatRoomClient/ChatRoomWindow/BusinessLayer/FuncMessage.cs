using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomWindow.PersistentLayer;


namespace ChatRoomWindow.BusinessLayer
{
    [Serializable]
    public class FuncMessage
    {
        //fields
        private String _messageBody;
        public String messageBody { get { return _messageBody; } }


        private User _user;
        public User user { get { return _user; } }

        private Guid GUID;
        public Guid guid { get { return GUID; } }

        private DateTime timeStamp;
        public DateTime dateTime { get { return timeStamp; } }

        private MessageHandler messageHand;

        private bool editable;
        private DataMessage m;

        public bool Editable
        {
            get { return editable; }
            set { editable = value; }
        }

        //consructor
        internal FuncMessage( String msgBody,Guid msgGuid, DateTime msgDateTime, User user)
        {
            this.GUID = msgGuid;
            this.timeStamp = msgDateTime;
            this._messageBody = msgBody;
            this._user = user;
            this.editable = false;
            messageHand = new MessageHandler();

        }
        //Static create (Misunderstanding the Factory Design Pattern)
        public static FuncMessage Create(String msg, User user)
        {
            if (ExceptionHandler.CheckValidty(msg))
            {
                Guid msgGuid = Guid.NewGuid();
                DateTime msgDateTime = DateTime.Now.ToLocalTime();
                return new FuncMessage(msg, msgGuid, msgDateTime, user);
            }
            else throw new Exception("Message Too Long!");
        }


        public override String ToString()
        {
            return "Group: " + _user.G_id_int + ", Nickname: " + _user.nickName + " (" + timeStamp + ") GUID: " + guid + " \n " + _messageBody + " \n";
        }
        //Saves message to the DB
        public void Send()
        {
            DataMessage dm = new DataMessage(this);
            messageHand.sqlSave(dm);
        }
        //Edit Message on the DB
        internal void replace()
        {
            try
            {
                messageHand.replace(this);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
