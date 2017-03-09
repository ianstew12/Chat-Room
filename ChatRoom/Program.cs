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
    class Program
    {
            static void Main(string[] args)
        {
            try
            {
                Server server = new Server();      
                server.StartServer();   
                server.RunServer();
            }

            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();

        }
    }

}