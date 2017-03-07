using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                TcpClient serverTPC = new TcpClient();
                IPAddress localAddr = IPAddress.Parse("192.168.0.137");
                serverTPC.Connect(localAddr, 2017);
                //TcpListener server = null;
                Console.Read();
            }
        }
    }
}
