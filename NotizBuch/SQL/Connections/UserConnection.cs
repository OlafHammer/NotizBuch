using NotizBuch.Models;
using NotizBuch.SQL.Main;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NotizBuch.SQL.Connections
{
    public class UserConnection
    {

        // Überprüft ob die Nutzerdaten in der Datenbank
        public static bool CheckAuth(UserCredentials credentials)
        {
            string sqlStatement = "SELECT * FROM dbo.Auth WHERE username = @Username AND password = @Password";

            using (SqlConnection connection = new SqlConnection(SQLConstants.ServerConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Username", credentials.Username);
                command.Parameters.AddWithValue("@Password", credentials.EncryptPassword());

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    return false;
                }
            }
        }

        // Legt einen neuen Nutzer an
        public static void Insert(UserCredentials credentials)
        {
            string sqlStatement = "INSERT INTO dbo.Auth VALUES(@Username, @Password)";

            using (SqlConnection connection = new SqlConnection(SQLConstants.ServerConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Username", credentials.Username);
                command.Parameters.AddWithValue("@Password", credentials.EncryptPassword());

                try
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
