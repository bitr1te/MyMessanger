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
using static Azure.Core.HttpHeader;
using System.Text;

namespace Client
{
    public class MessClient : ViewModelBase
    {
        public string Nick { get; set; } = Properties.Settings.Default.Nick;
        public int TargetId { get; set; } = Properties.Settings.Default.TargetId;
        public int ID { get; set; } = Properties.Settings.Default.ID;
        public string Message { get => GetValue<string>(); set => SetValue(value); }
        public string Chat { get => GetValue<string>(); set => SetValue(value); }

        public List<Chat> chats = new List<Chat>();

        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;

        public MessClient()
        {
            Nick = Properties.Settings.Default.Nick;
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
                                if (line.Contains("#"))
                                {
                                    Chat = "";
                                    line = line.Remove(0, 1);
                                    string[] result = line.Split('*');
                                    foreach (string s in result)
                                    {
                                        Chat += $"{s}\n";
                                    }
                                }
                                TargetId = Properties.Settings.Default.TargetId;
                                _writer = new StreamWriter(_client.GetStream());
                                _writer.AutoFlush = true;
                                _writer?.WriteLine($"##:{TargetId}");
                            }
                            else
                            {
                                _client.Close();
                                MessageBox.Show("Connected error");
                                break;
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

                            Nick = Properties.Settings.Default.Nick;
                            ID = Properties.Settings.Default.ID;
                            _writer.WriteLine($"Login: {ID}");
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
                        Nick = Properties.Settings.Default.Nick;
                        TargetId = Properties.Settings.Default.TargetId;

                        if ((Message == "" || Message is null) && TargetId != -1)
                        {
                            _writer?.WriteLine($"{TargetId}:{Nick}: Я в сети!");
                        }
                        else
                        {
                            _writer?.WriteLine($"{TargetId}:{Nick}: {Message}");
                        }
                        Message = "";
                    });
                }, () => _client?.Connected == true, !string.IsNullOrWhiteSpace(Message));
            }
        }
    }
}
