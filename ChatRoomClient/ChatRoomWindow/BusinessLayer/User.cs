using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomWindow.PersistentLayer;

namespace ChatRoomWindow.BusinessLayer
{
    [Serializable]
    public class User
    {
        //fields
        private int id;
        public int Id { get { return id; } set { id = value;} }


        private String _nickName;
        public String nickName { get { return _nickName; } }

        private int g_id_int;
        public int G_id_int { get { return g_id_int; } }

        private string hash_ps;
        public String password { get { return hash_ps; } }

        private UserHandler userHand;

        //constructors
        public User(string nickname, int g_id_int, string hash_ps)
        {
            this._nickName = nickname;
            this.g_id_int = g_id_int;
            this.hash_ps = hash_ps;
            this.userHand = new UserHandler();
        }
        public User(string nickname, int g_id_int, string hash_ps, int UserId)
        {
            this._nickName = nickname;
            this.g_id_int = g_id_int;
            this.hash_ps = hash_ps;
            this.id = UserId;
            this.userHand = new UserHandler();
        }

        //Checks equality between two users by their G_id and nicknames. 
        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                User user = (User)obj;
                return ((this.G_id_int == user.G_id_int) && (this.nickName == user.nickName));
            }

            return false;
        }

        //Register the user
        public void Register()
        {
            if (!isExist())
            {
                userHand.SqlUserSaver(this);
                
            }
            else
                throw new Exception("Error! The User is Already Exist!");
        }

        //login the user
        internal void Login()
        {
            String tData = userHand.getUser(nickName, G_id_int);
            if (tData == "")
                throw new Exception("Error :user does not exist , try to register before");

            int commaPlace = tData.IndexOf(',');
            String pass = tData.Substring(commaPlace + 1);
            if (!pass.Equals(password))
                throw new Exception("Error: wrong password");
            Id = int.Parse(tData.Substring(0, commaPlace));
        }

        //Static create (Misunderstanding the Factory Design Pattern)
        public static User create (String nickname, String g_id,String hashPassword)
        {
            ExceptionHandler.NicknameExceptionHandeling(nickname);
            ExceptionHandler.G_idExceptionHandeling(g_id);
            

            int g_id_int = int.Parse(g_id);
            return new User(nickname, g_id_int, hashPassword);

        }


        //Check if user Exist on the DB
        public Boolean isExist()
        {
            return (!(userHand.getUser(nickName, G_id_int) == ""));
        }
        // Sends a message to the server and returns a Message object.
        public FuncMessage sendMessage(String msg)
        {
            try
            {
                FuncMessage sentMsg = FuncMessage.Create(msg, this);
                sentMsg.Send();
                return sentMsg;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }

}

