using DevExpress.Mvvm;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Mail;

namespace Client
{
    public class MessClient : ViewModelBase
    {
        public string Nick { get; set; } = Properties.Settings.Default.Nick;
        public string Message { get => GetValue<string>(); set => SetValue(value); }
        public string Target { get => GetValue<string>(); set => SetValue(value); }
        public string Chat
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public List<Chat> chats = new List<Chat>();

        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;

        public void RefreshChats()
        {
            string con = "Data Source=DESKTOP-OCDVJBU\\SQLEXPRESS02;Initial Catalog=OurMessangerDB;Integrated Security=True;TrustServerCertificate=true;";
            SqlConnection connection = new SqlConnection(con);

            connection.Open();

            var cmd = new SqlCommand("SELECT Member1, Member2 FROM Chats JOIN Users ON ID_user = Member1 WHERE Name = @Name", connection);
            cmd.Parameters.AddWithValue("@Name", Nick);
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read()) // Нужен цикл, а не условие
                {
                    Chat chat = new Chat((int)rd["Member1"], (int)rd["Member2"]);
                    chats.Add(chat);
                }
            }
        }

        public MessClient()
        {
            Nick = Properties.Settings.Default.Nick;
            RefreshChats();
        }
        public MessClient(string Nick)
        {
            this.Nick = Nick;
        }

        private void Listener()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (_client?.Connected == true)
                        {
                            var line = _reader?.ReadLine();
                            if (line != null)
                            {
                                if(line.Contains("###"))
                                {
                                    MessageBox.Show("Пользователя не существует!");
                                    continue;
                                }
                                RefreshChats();
                            }
                            else
                            {
                                _client.Close();
                                MessageBox.Show("Connected error");
                            }
                        }
                        Task.Delay(10).Wait();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });
        }
        public AsyncCommand ConnectCommand
        {
            get
            {
                return new AsyncCommand(() =>
                {
                    return Task.Run(() =>
                    {
                        try
                        {
                            _client = new TcpClient();
                            _client.Connect("127.0.0.1", 5050);
                            _reader = new StreamReader(_client.GetStream());
                            _writer = new StreamWriter(_client.GetStream());
                            Listener();
                            _writer.AutoFlush = true;

                            _writer.WriteLine($"Login: {Nick}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    });
                }, () => _client is null || _client?.Connected == false);
            }
        }

        public AsyncCommand SendCommand
        {
            get
            {
                return new AsyncCommand(() =>
                {
                    return Task.Run(() =>
                    {
                        _writer?.WriteLine($"{Target}:{Nick}: {Message}");
                        Message = "";
                    });
                }, () => _client?.Connected == true, !string.IsNullOrWhiteSpace(Message));
            }
        }
    }
}
