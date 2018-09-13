using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWindow.BusinessLayer
{
    //Class of Static functions that manage Exceptions
    class ExceptionHandler
    {
        //Check Legal GRoup Id
        public static void G_idExceptionHandeling(String G_ID)
        {
            if (G_ID == "")
                throw new ArgumentException("Error! Please fill GroupId:");

            if (!ValidG_id(G_ID))//check if GroupId contains only numbers
                throw new ArgumentException("Error! The Group id can contain only Numbers");
        }      
        private static bool ValidG_id(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        

       //Check Legal nickname
        public static void NicknameExceptionHandeling(String nickName)
        {
            if (nickName == "")
                throw new ArgumentException("Error! Please fill NickName");

            if (nickName.Contains(" "))
                throw new ArgumentException("Error! The nickName cannot contain spaces.");
        }

        //Check Legal Password
        public static void passwordExceptionHandeling(String password)
        {

            if (password == "")
                throw new ArgumentException("Error! Please fill Bussword");
            if (password.Contains(" "))
                throw new ArgumentException("Error! The Bussword cannot contain spaces.");
            foreach (char c in password)
            {
                if ((c < '0' || c > '9') && (c < 'a' || c > 'z') && (c < 'A' || c > 'Z'))
                    throw new ArgumentException("Error! The Bussword can contain only letters");
            }

            if (password.Length < 4 || password.Length > 16)
                throw new ArgumentException($"Error! Password length need to be between 4 to 16 chars: {password}");


        }
        public static bool isValidPass(String password)
        {

            if (password == "")
                return false;
            if (password.Contains(" "))
                return false;
            foreach (char c in password)
            {
                if ((c < '0' || c > '9') && (c < 'a' || c > 'z') && (c < 'A' || c > 'Z'))
                    return false;
            }

            if (password.Length < 4 || password.Length > 16)
                return false;

            return true;
        }



        //Checks if message is legal, contains less than 150 characters and not empty
        public static Boolean CheckValidty(String txt)
        {
            return (txt.Length <= 100 && (txt.Trim().Count() != 0));
        }
    }
}
