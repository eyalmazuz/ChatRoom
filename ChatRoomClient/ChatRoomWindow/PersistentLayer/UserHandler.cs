using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ChatRoomWindow.BusinessLayer;
using System.Data.SqlClient;
using System;
using System.Data;

namespace ChatRoomWindow.PersistentLayer
{
    public class UserHandler
    {
        private String UserData = "UserData.bin";
        
        string sql_query = null;
        private static string server_address = "ise172.ise.bgu.ac.il,1433\\DB_LAB";
        private static string database_name = "MS3";
        private static string user_name = "publicUser";
       private static string password = "isANerd";
        SqlConnection connection;
        SqlCommand command;
        private String connetion_string = $"Data Source={server_address};Initial Catalog={database_name };User ID={user_name};Password={password}";

        //Gets user Password from the DB
        public String getUser(String nickname, int g_id) {
            String output = "";
            connection = new SqlConnection(connetion_string);
            SqlDataReader data_reader;
            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);
                sql_query = $"SELECT * FROM [Users] WHERE Nickname = '{nickname}' AND Group_Id='{g_id}'"; 
                command = new SqlCommand(sql_query, connection);
                data_reader = command.ExecuteReader();
                while (data_reader.Read())
                {
                    output =data_reader.GetValue(0)+","+data_reader.GetValue(3);
                }
                data_reader.Close();
                command.Dispose();
                connection.Close();
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;

        }
        //Saves users to the DB
        public void SqlUserSaver(User user)
        {
           
            connection = new SqlConnection(connetion_string);
           
            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);


                command.CommandText =
                    "INSERT INTO Users ([Group_Id],[NickName],[Password]) " +
                    "VALUES ( @group_id, @nick_name, @password)";
                
                SqlParameter group_id_param = new SqlParameter(@"group_id", SqlDbType.Int, 20);
                SqlParameter nick_name_param = new SqlParameter(@"nick_name", SqlDbType.Char, 8);
                SqlParameter password_param = new SqlParameter(@"password", SqlDbType.Char, 64);


                group_id_param.Value = user.G_id_int;
                nick_name_param.Value = user.nickName;
                password_param.Value =  user.password;

               
                command.Parameters.Add(group_id_param);
                command.Parameters.Add(nick_name_param);
                command.Parameters.Add(password_param);

                // Call Prepare after setting the Commandtext and Parameters.
                command.Prepare();
                int num_rows_changed = command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


    }
}
