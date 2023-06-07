using DevExpress.Mvvm;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    public class MessClient : ViewModelBase
    {
        public string Nick { get; set; } = "Nick";
        public string Message { get => GetValue<string>(); set => SetValue(value); }
        public string Target { get => GetValue<string>(); set => SetValue(value); }

        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;

        public MessClient()
        {

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
                                //
                            }
                            else
                            {
                                _client.Close();
                            }
                        }
                        Task.Delay(10).Wait();
                    }
                    catch (Exception ex)
                    {
                        //
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
