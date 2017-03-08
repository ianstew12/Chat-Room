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
        IPAddress ipAddress;
        TcpListener server;
        int port = 2017;
        public Dictionary<string, Client.Client> connectedClients =
            new Dictionary<string, Client.Client>();

        public Server()
        {
            ipAddress = GetLocalIPAddress();
            server = new TcpListener(ipAddress, port);             //concrete subject?
        }

        public static IPAddress GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipAddress in host.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddress;
                }
            }
            throw new Exception("IP Address Not Found!");
        }


        //public void Attach(Client.Client clientToAttach)
        //{
        //    connectedClients.Add(clientToAttach.Name, clientToAttach);
        //    NotifyOfEntry(clientToAttach);
        //}

        //public void Detach(Client.Client clientToDetach)
        //{
        //    connectedClients.Remove(clientToDetach.Name);
        //    NotifyOfExit(clientToDetach);
        //}

        //public void NotifyOfEntry(Client.Client recentlyJoinedClient)
        //{
        //    foreach (KeyValuePair<string, Client.Client> client in connectedClients)
        //    {
        //        client.Value.ReceiveMessage(recentlyJoinedClient.Name + " has entered the chat");
        //    }
        //}

        //public void NotifyOfExit(Client.Client recentlyExitedClient)
        //{
        //    foreach (KeyValuePair<string, Client.Client> client in connectedClients)
        //    {
        //        client.Value.ReceiveMessage(recentlyExitedClient.Name + " has left the chat");
        //    }
        //}
        //in program class I will create a server, start it, then tell it to listen:
        public void StartServer()
        {
            server.Start();
        }


        public void ListenForClients()
        {
            Byte[] bytes = new Byte[256];
            String data = null;
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.

                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                //listening for messages
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    //Encode to UTF8 (not ASCII)
                    data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();
                    byte[] msg = System.Text.Encoding.UTF8.GetBytes(data);


                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);

                   // stream.Close();
                    client.Close();
                }
            }
        }

        Queue<Message> messages = new Queue<Message>();



    }
}
