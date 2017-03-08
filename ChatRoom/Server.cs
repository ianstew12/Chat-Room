using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Client;
namespace ChatRoom
{
    class Server
    {
        //int port;
        //IPAddress ipAddress;
        //TcpListener tcpListener;
        public Dictionary<string, Client.Client> connectedClients = new Dictionary<string, Client.Client>();

    }
}
