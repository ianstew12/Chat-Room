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
    public class Client :  TcpClient, INotifiable
    {
        private string name;
        IPAddress ipAddress;


        public Client()
        {
            ipAddress = GetLocalIPAddress();
            name = RequestName();
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
    
        public string RequestName()
        {
            Console.WriteLine("Enter your name");
            return name = Console.ReadLine();
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public void SendMessage(string messageToSend)//, IPAddress serverIPAdress)
        {

            Byte[] data = System.Text.Encoding.UTF8.GetBytes(messageToSend);

            NetworkStream stream = GetStream();
            
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", messageToSend);
        }

        public void ReceiveMessage(string messageReceived)
        {
            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            Byte[] data = System.Text.Encoding.UTF8.GetBytes(messageReceived);

            NetworkStream stream = GetStream();
            data = new Byte[256];

            // String to store the response UTF8 representation.
            String responseData = String.Empty;
            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);
        }

    }
}
