using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace ChatRoom
{
    class Client
    {
        private string name;
        IPAddress ipAddress;
        public TcpClient client;
        //int port = 2017;


        public Client(IPAddress ipAddress, int port)
        {
            
            client = new TcpClient(ipAddress.ToString(), port);
        }

        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipAddress in host.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddress;
                }
            }
            throw new Exception("IP Address Not Found!");
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
