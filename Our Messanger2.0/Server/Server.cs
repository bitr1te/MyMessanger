using DevExpress.Mvvm.UI;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPChat.Server
{
    internal class Program
    {
        static TcpListener listener = new(IPAddress.Any, 5050);
        static List<ConnectedClient> clients = new();
        static string nick = "";
        static void Main(string[] args)
        {
            listener.Start();

            Console.WriteLine(listener.LocalEndpoint);
            while (true)
            {
                var client = listener.AcceptTcpClient();
                int ID = -1;
                Task.Factory.StartNew(() =>
                {
                    ConnectedClient cli = new ConnectedClient(client, ID, nick);
                    if (!client.Connected)
                    {
                        clients.Remove(cli);
                    }
                    var sr = new StreamReader(client.GetStream());
                    while (client.Connected)
                    {
                        var line = sr.ReadLine();

                        ID = Int32.Parse(line.Replace("Login: ", ""));

                        if (line.Contains("Login: ") && !string.IsNullOrWhiteSpace(ID.ToString()))
                        {
                            if (clients.FirstOrDefault(s => s.ID == ID) is null)
                            {
                                clients.Add(cli);
                                Console.WriteLine($"Новое подключение: {ID}");
                                break;
                            }
                            else
                            {
                                var sw = new StreamWriter(client.GetStream());
                                sw.AutoFlush = true;
                                sw.WriteLine("Пользователь с таким ID уже есть");
                                client.Client.Disconnect(false);
                            }
                        }
                    }

                    while (client.Connected)
                    {
                        try
                        {
                            var line = sr.ReadLine();

                            StringBuilder sb = new StringBuilder();
                            int c = 0;
                            while (true)
                            {
                                if (line[c] == ':')
                                    break;
                                sb.Append(line[c]);
                                c++;
                            }
                            c++;
                            line = line.Remove(0, c);
                            var targetId = sb.ToString();
                            if (sb.ToString() == "##")
                                targetId = line;



                            string target = "";

                            string connectionString = "Data Source=DESKTOP-OCDVJBU\\SQLEXPRESS02;Initial Catalog=OurMessangerDB;Integrated Security=True;TrustServerCertificate=true;";

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();

                                string getNick = "SELECT Name FROM Users WHERE ID_user = @ID";
                                using (SqlCommand command = new SqlCommand(getNick, connection))
                                {
                                    command.Parameters.AddWithValue("@ID", ID);

                                    nick = (string)command.ExecuteScalar();
                                }

                                string getChatId = "SELECT ID_chat FROM Chats WHERE Member1 = @Member1 AND Member2 = @Member2";
                                int id1 = -1;
                                int id2 = -1;
                                using (SqlCommand command = new SqlCommand(getChatId, connection))
                                {
                                    command.Parameters.AddWithValue("@Member1", ID);
                                    command.Parameters.AddWithValue("@Member2", targetId);

                                    if (command.ExecuteScalar() is null)
                                    {
                                        string insertChat1 = "INSERT INTO Chats (Member1, Member2) VALUES (@Owner, @Target)";

                                        using (SqlCommand command1 = new SqlCommand(insertChat1, connection))
                                        {
                                            command1.Parameters.AddWithValue("@Owner", ID);
                                            command1.Parameters.AddWithValue("@Target", targetId);

                                            int rowsAffected = command1.ExecuteNonQuery();
                                        }
                                        string insertChat2 = "INSERT INTO Chats (Member1, Member2) VALUES (@Owner, @Target)";
                                        using (SqlCommand command1 = new SqlCommand(insertChat2, connection))
                                        {
                                            command1.Parameters.AddWithValue("@Owner", targetId);
                                            command1.Parameters.AddWithValue("@Target", ID);

                                            int rowsAffected = command1.ExecuteNonQuery();
                                        }
                                    }
                                    id1 = (int)command.ExecuteScalar();
                                }
                                using (SqlCommand command = new SqlCommand(getChatId, connection))
                                {
                                    command.Parameters.AddWithValue("@Member1", targetId);
                                    command.Parameters.AddWithValue("@Member2", ID);

                                    id2 = (int)command.ExecuteScalar();
                                }

                                if (sb.ToString() == "##")
                                {
                                    List<string> messages = new List<string>();
                                    string getMessages = "SELECT Message FROM Messages WHERE ID_chat = @Id1 ORDER BY Date";
                                    SqlCommand sqlCommand = new SqlCommand(getMessages, connection);
                                    sqlCommand.Parameters.AddWithValue("@Id1", id1);

                                    using (var rd = sqlCommand.ExecuteReader())
                                    {
                                        while (rd.Read())
                                        {
                                            messages.Add($"{rd["Message"]}");
                                        }
                                    }
                                    var chatSend = new StreamWriter(client.GetStream());
                                    chatSend.AutoFlush = true;
                                    chatSend.WriteLine("#" + String.Join("*", messages));
                                    continue;
                                }

                                string getTargetName = "SELECT Name FROM Users WHERE ID_user = @targetId";

                                using (SqlCommand command = new SqlCommand(getTargetName, connection))
                                {
                                    command.Parameters.AddWithValue("@targetId", targetId);

                                    target = (string)command.ExecuteScalar();
                                }

                                string query2 = "INSERT INTO Messages (ID_chat, Message, Date, Owner) VALUES (@id1, @line, @Date, @nick)";

                                using (SqlCommand command = new SqlCommand(query2, connection))
                                {
                                    command.Parameters.AddWithValue("@id1", id1);
                                    command.Parameters.AddWithValue("@line", line);
                                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                                    command.Parameters.AddWithValue("@nick", nick);

                                    int rowsAffected = command.ExecuteNonQuery();
                                }
                                using (SqlCommand command = new SqlCommand(query2, connection))
                                {
                                    command.Parameters.AddWithValue("@id1", id2);
                                    command.Parameters.AddWithValue("@line", line);
                                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                                    command.Parameters.AddWithValue("@nick", nick);

                                    int rowsAffected = command.ExecuteNonQuery();
                                }
                                var sw = new StreamWriter(client.GetStream());
                                sw.AutoFlush = true;
                                sw.WriteLine("");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                });
            }
        }

        //private static void SendToClient(string message, string nick)
        //{
        //    Task.Run(() =>
        //    {
        //        for (int i = 0; i < clients.Count; i++)
        //        {
        //            try
        //            {
        //                if (clients[i].Client.Connected)
        //                {
        //                    if (clients[i].Name == nick)
        //                    {
        //                        var sw = new StreamWriter(clients[i].Client.GetStream());
        //                        sw.AutoFlush = true;
        //                        sw.WriteLine(message);
        //                    }
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"{clients[i].Name} отключился");
        //                    clients.RemoveAt(i);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //            }
        //        }
        //    });
        //}
    }
}
