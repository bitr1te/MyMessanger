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
        static void Main(string[] args)
        {
            listener.Start();

            Console.WriteLine(listener.LocalEndpoint);
            while (true)
            {
                var client = listener.AcceptTcpClient();
                string nick = "";
                Task.Factory.StartNew(() =>
                {
                    var sr = new StreamReader(client.GetStream());
                    while (client.Connected)
                    {
                        var line = sr.ReadLine();
                        nick = line.Replace("Login: ", "");

                        if (line.Contains("Login: ") && !string.IsNullOrWhiteSpace(nick))
                        {
                            if (clients.FirstOrDefault(s => s.Name == nick) is null)
                            {
                                clients.Add(new ConnectedClient(client, nick));
                                Console.WriteLine($"Новое подключение: {nick}");
                                break;
                            }
                            else
                            {
                                var sw = new StreamWriter(client.GetStream());
                                sw.AutoFlush = true;
                                sw.WriteLine("Пользователь с таким ником уже есть");
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
                            var target = sb.ToString();

                            string connectionString = "Data Source=DESKTOP-OCDVJBU\\SQLEXPRESS02;Initial Catalog=OurMessangerDB;Integrated Security=True;TrustServerCertificate=true;";

                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();

                                string query1 = "SELECT ID_chat FROM Chats WHERE Member1 = @Member1 AND Member2 = @Member2";
                                string getOwnerId = "SELECT ID_user FROM Users WHERE Name = @Nick";
                                string getTargetId = "SELECT ID_user FROM Users WHERE Name = @Target";

                                int ownerId = -1;
                                int targetId = -1;
                                using (SqlCommand command = new SqlCommand(getOwnerId, connection))
                                {
                                    command.Parameters.AddWithValue("@Nick", nick);

                                    ownerId = (int)command.ExecuteScalar();
                                }

                                using (SqlCommand command = new SqlCommand(getTargetId, connection))
                                {
                                    command.Parameters.AddWithValue("@Target", target);

                                    if(command.ExecuteScalar() is null)
                                    {
                                        var sw1 = new StreamWriter(client.GetStream());
                                        sw1.AutoFlush = true;
                                        sw1.WriteLine("###");
                                        Console.WriteLine("Попытка отправить сообщение несуществующему пользователю!");
                                        continue;
                                    }
                                    targetId = (int)command.ExecuteScalar();
                                }

                                int id1 = -1;
                                int id2 = -1;
                                using (SqlCommand command = new SqlCommand(query1, connection))
                                {
                                    command.Parameters.AddWithValue("@Member1", ownerId);
                                    command.Parameters.AddWithValue("@Member2", targetId);

                                    if(command.ExecuteScalar() is null)
                                    {
                                        string insertChat1 = "INSERT INTO Chats (Member1, Member2) VALUES (@Owner, @Target)";

                                        using (SqlCommand command1 = new SqlCommand(insertChat1, connection))
                                        {
                                            command1.Parameters.AddWithValue("@Owner", ownerId);
                                            command1.Parameters.AddWithValue("@Target", targetId);

                                            int rowsAffected = command1.ExecuteNonQuery();
                                        }
                                        string insertChat2 = "INSERT INTO Chats (Member1, Member2) VALUES (@Owner, @Target)";
                                        using (SqlCommand command1 = new SqlCommand(insertChat2, connection))
                                        {
                                            command1.Parameters.AddWithValue("@Owner", targetId);
                                            command1.Parameters.AddWithValue("@Target", ownerId);

                                            int rowsAffected = command1.ExecuteNonQuery();
                                        }
                                    }
                                    id1 = (int)command.ExecuteScalar();
                                }
                                using (SqlCommand command = new SqlCommand(query1, connection))
                                {
                                    command.Parameters.AddWithValue("@Member1", targetId);
                                    command.Parameters.AddWithValue("@Member2", ownerId);

                                    id2 = (int)command.ExecuteScalar();
                                }

                                string query2 = "INSERT INTO Messages (ID_chat, Message, Date, Owner) VALUES (@id1, @line, @Date, @nick)";

                                using (SqlCommand command = new SqlCommand(query2, connection))
                                {
                                    command.Parameters.AddWithValue("@id1", id1);
                                    command.Parameters.AddWithValue("@line", line);
                                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                                    command.Parameters.AddWithValue("@nick", ownerId);

                                    int rowsAffected = command.ExecuteNonQuery();
                                }
                                using (SqlCommand command = new SqlCommand(query2, connection))
                                {
                                    command.Parameters.AddWithValue("@id1", id2);
                                    command.Parameters.AddWithValue("@line", line);
                                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                                    command.Parameters.AddWithValue("@nick", ownerId);

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
