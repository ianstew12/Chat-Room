using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IPAddress address = IPAddress.Parse("192.168.0.137");
                Client client = new Client(address, 2017);
                client.GetConnection();
                client.StartClient();



                //IPAddress address = IPAddress.Parse(ipAddress);
                ////create connection
                //int port = 2017;
                //TcpClient client = new TcpClient("192.168.0.137", port);


                //string message = "Hello, are you there?";

                //Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);

                //// Get a client stream for reading and writing.

                //NetworkStream stream = client.GetStream();

                //// Send the message to the connected TcpServer. 
                //stream.Write(data, 0, data.Length);

                //Console.WriteLine("Sent: {0}", message);

                //// Receive the TcpServer.response.

                //// Buffer to store the response bytes.
                //data = new Byte[256];

                //// String to store the response UTF8 representation.
                //String responseData = String.Empty;
                //// Read the first batch of the TcpServer response bytes.
                //Int32 bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                //Console.WriteLine("Received: {0}", responseData);

                //// Close everything.
                //stream.Close();
                //client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
