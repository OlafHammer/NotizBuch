
using NotizBuch.Models;
using NotizBuch.SQL.Main;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NotizBuch.SQL.Connections
{

    // Implementiert die Basis SQL Methoden für die Notes Klasse
    public class NotesConnection : ISQLFunctions<Notes>
    {
        public void Delete(int id)
        {
            string sqlStatement = "DELETE FROM dbo.Notes WHERE id = @Id";

            using (SqlConnection connection = new SqlConnection(SQLConstants.ServerConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id", id);

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

        public void Edit(Notes notes)
        {
            string sqlStatement = "UPDATE dbo.Notes SET " +
                "username = @Username, " +
                "creation_time = @CreationTime, " +
                "title = @Title, " +
                "text = @Text " +
                "WHERE id = @Id";

            using (SqlConnection connection = new SqlConnection(SQLConstants.ServerConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Username", notes.Username);
                command.Parameters.AddWithValue("@CreationTime", notes.CreationTime);
                command.Parameters.AddWithValue("@Title", notes.Title);
                command.Parameters.AddWithValue("@Text", notes.Text);
                command.Parameters.AddWithValue("@Id", notes.Id);
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

        public Notes Get(int id)
        {
            string sqlStatement = "SELECT * FROM dbo.Notes WHERE id = @Id";

            using (SqlConnection connection = new SqlConnection(SQLConstants.ServerConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Notes() {
                            Id =  (int) reader[0],
                            Username =  (string) reader[1],
                            CreationTime =  (DateTime) reader[2],
                            Title =  (string) reader[3],
                            Text =  (string) reader[4]
                        };

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return null;
        }

        public List<Notes> GetAll()
        {
            List<Notes> notes = new List<Notes>();

            string sqlStatement = "SELECT * FROM dbo.Notes";

            using (SqlConnection connection = new SqlConnection(SQLConstants.ServerConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        notes.Add(new Notes()
                        {
                            Id = (int)reader[0],
                            Username = (string)reader[1],
                            CreationTime = (DateTime)reader[2],
                            Title = (string)reader[3],
                            Text = (string)reader[4]
                        });

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return notes;
        }

        public void Insert(Notes notes)
        {
            string sqlStatement = "INSERT INTO dbo.Notes VALUES(@Username, @CreationTime, @Title, @Text)";

            using (SqlConnection connection = new SqlConnection(SQLConstants.ServerConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Username", notes.Username);
                command.Parameters.AddWithValue("@CreationTime", notes.CreationTime);
                command.Parameters.AddWithValue("@Title", notes.Title);
                command.Parameters.AddWithValue("@Text", notes.Text);

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
