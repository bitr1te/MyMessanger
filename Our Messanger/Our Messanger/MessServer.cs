using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Our_Messanger
{
    internal class MessServer
    {
        private bool run = true;
        private int port = 8080;
        private TcpListener server = null;

        public MessServer(bool run, int port)
        {
            try
            {
                this.run = run;
                this.server = new TcpListener(IPAddress.Any, port);
            }
            catch 
            {
                MessageBox.Show("Ошибка ввода данных сервера!");
            }
        }

        public void SetRun(bool run)
        {
            this.run = run;
            if (run == true)
                ListenAsync();
            else
                server.Stop();
        }

        public void SetPort(int port)
        {
            run = false;
            this.port = port;
            server = new TcpListener(IPAddress.Any, this.port);
        }

        public void Start() 
        {
            run = true;
            ListenAsync();
        }

        public void Stop()
        {
            run = false;
        }

        public async Task ListenAsync()
        {
            server.Start();
            // Выполняем цикл только, если серверная часть «включена»
            while (run)
            {
                try
                {
                    // Асинхронное подключение клиента
                    TcpClient client = await server.AcceptTcpClientAsync();
                    NetworkStream stream = client.GetStream();
                    // Обмен данными только, если серверная часть «включена».
                    try
                    {
                        // Читаем данные
                        if (stream.CanRead && run)
                        {
                            byte[] myReadBuffer = new byte[1024];
                            StringBuilder myCompleteMessage = new StringBuilder();
                            int numberOfBytesRead = 0;
                            do
                            {
                                numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead));
                            }
                            while (stream.DataAvailable);
                            Byte[] responseData = Encoding.UTF8.GetBytes("УСПЕШНО!");
                            stream.Write(responseData, 0, responseData.Length);
                        }
                    }
                    finally
                    {
                        stream.Close();
                        client.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    server.Stop();
                    break;
                }
            }
            // Если серверная часть «выключена», обязательно останавливаем прослушивание порта.
            // Иначе потом серверная часть не «включится».
            if (!run)
            {
                server.Stop();
            }
        }
    }
}
