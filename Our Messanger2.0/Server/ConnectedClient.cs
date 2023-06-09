using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TCPChat.Server
{
    public class ConnectedClient
    {
        public TcpClient Client { get; set; }
        public int ID { get; set; }
        public string Nick { get; set; }

        public ConnectedClient(TcpClient client, int id, string nick)
        {
            Client = client;
            ID = id;
            Nick = nick;
        }
    }
}