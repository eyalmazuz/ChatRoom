using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ChatRoomWindow.BusinessLayer;
using System.Data.SqlClient;
using System.Data;

namespace ChatRoomWindow.PersistentLayer
{
    public class MessageHandler
    {
        //field
        private String MessageData = "MessageData.bin";
        string sql_query = null;
        private static string Server_Adress = "ise172.ise.bgu.ac.il,1433\\DB_LAB";
        private static string database_name = "MS3";
        private static string user_name = "publicUser";
        private static string password = "isANerd";
        SqlConnection connection;
        SqlCommand command;
        private String connetion_string = $"Data Source={Server_Adress};Initial Catalog={database_name };User ID={user_name};Password={password}";
        List<IQueryAction> filters = new List<IQueryAction>();
        SqlDataReader data_reader;

   
     
        
        //Edit a Message on the DB
        internal void replace(FuncMessage message)
        {
            connection = new SqlConnection(connetion_string);
            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);
                command.CommandText = $"update Messages  set [Body] = @body, [SendTime] = @send_time where Guid = @guid";
                SqlParameter guid_param = new SqlParameter(@"guid", SqlDbType.Char, 68);
                SqlParameter send_time_param = new SqlParameter(@"send_time", SqlDbType.DateTime, 64);
                SqlParameter body_param = new SqlParameter(@"body", SqlDbType.NChar, 100);

                guid_param.Value = message.guid.ToString().Trim();
                send_time_param.Value = message.dateTime.ToUniversalTime();
                body_param.Value = message.messageBody;


                command.Parameters.Add(guid_param);
                command.Parameters.Add(send_time_param);
                command.Parameters.Add(body_param);


                command.Prepare();

                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception exe)
            {
                throw exe;
            }
        }
        //Returns a List of messages from the DB
        public List<DataMessage> getMessageList(String dt)
        {
            List<DataMessage> messageList = new List<DataMessage>();
            connection = new SqlConnection(connetion_string);
            String sql_query;
            try
            {
                connection.Open();
                // Console.WriteLine("connected to: " + server_address);
                //  if(dt == null)
                sql_query = $"select TOP 200 * from [dbo].[Users] JOIN [dbo].[Messages]  on [Messages].User_Id = [Users].Id ";
                if (dt != null)
                    sql_query += $"WHERE SendTime>'{dt}' ";
                foreach (IQueryAction action in filters)
                {
                    sql_query = action.execute(sql_query);
                }
                sql_query += "Order by SendTime DESC;";
                command = new SqlCommand(sql_query, connection);
                data_reader = command.ExecuteReader();
                while (data_reader.Read())
                {
                    Guid G_id = new Guid(data_reader.GetValue(4).ToString());
                    User user = new User(data_reader.GetValue(2).ToString().Trim(), int.Parse(data_reader.GetValue(1).ToString().Trim()), data_reader.GetValue(3).ToString().Trim(), int.Parse(data_reader.GetValue(0).ToString().Trim()));
                    DateTime date = data_reader.GetDateTime(6).ToLocalTime();
                    String body = data_reader.GetValue(7).ToString().Trim();
                    DataMessage m = new DataMessage(body, G_id, date, user);


                    messageList.Add((m));

                }
                data_reader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());
            }
            return messageList;

        }

        //Saves a Message to the DB
        public void sqlSave(DataMessage message)
        {
            connection = new SqlConnection(connetion_string);

            try
            {
                connection.Open();
                command = new SqlCommand(null, connection);

                // Create and prepare an SQL statement.
                // Use should never use something like: query = "insert into table values(" + value + ");" 
                // Especially when selecting. More about it on the lab about security.
                command.CommandText =
                    "INSERT INTO Messages ([Guid],[User_Id],[SendTime],[Body]) " +
                    "VALUES ( @guid, @user_id, @send_time,@body)";

                SqlParameter guid_param = new SqlParameter(@"guid", SqlDbType.Char, 68);
                SqlParameter user_id_param = new SqlParameter(@"user_id", SqlDbType.Int, 20);
                SqlParameter send_time_param = new SqlParameter(@"send_time", SqlDbType.DateTime, 64);
                SqlParameter body_param = new SqlParameter(@"body", SqlDbType.NChar, 100);

                // get top 1 by id

                guid_param.Value = message.guid.ToString();
                user_id_param.Value = message.user.Id;
                send_time_param.Value = message.dateTime.ToUniversalTime();
                body_param.Value = message.messageBody;


                command.Parameters.Add(guid_param);
                command.Parameters.Add(user_id_param);
                command.Parameters.Add(send_time_param);
                command.Parameters.Add(body_param);

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

        //Manage the filter Querries
        public void AddGroupFilter(int Group_id)
        {
            GroupFilter g_filter = new GroupFilter(Group_id);
            filters.Add(g_filter);
        }
        public void AddNicknameFilter(String Nickname)
        {
            NicknameFilter n_filter = new NicknameFilter(Nickname);
            filters.Add(n_filter);
        }
        public void clearFilters()
        {
            filters.Clear();
        }
    }
}

