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
    class Server
    {
        IPAddress ipAddress;
        TcpListener server;
        int port = 2017;

        public Dictionary<TcpClient,string> connectedClients =
            new Dictionary<TcpClient,string >();

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




        public void StartServer()
        {
            server.Start();
        }
        
        public void RunServer()
        {
            ListenForClients();
        }

        public void ListenForClients()
        {
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                //HERE I WANT TO ADD TO DICTIONARY AND START NEW THREAD

                
                TcpClient client = server.AcceptTcpClient();
                //receive message client name from client
                //add client and client name to dictionary
                //ReceiveMessageFromClient(client);
                connectedClients.Add(client, "placeHolder");

                Console.WriteLine("Connected!");
                Task listenForMessages = Task.Run(() => ListenForMessages(client));
               
            }
        }

        public void ReceiveMessageFromClient(TcpClient client)
        {
            NetworkStream stream = GetClientStream(client);

            
        }
        public NetworkStream GetClientStream(TcpClient client)
        {
             NetworkStream stream = client.GetStream();
            return stream;
        }
        

        public void ListenForMessages(TcpClient client)
        {
            Byte[] bytes = new Byte[256];
            String data = null;
            NetworkStream stream = client.GetStream();
            int i;
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

            }
            //stream.Close();
               client.Close();
        }
        //public string ParseMessage (string message)
        //{
        //    //if first characters are formatted name change dictionary name
        //    string firstFourteen = message.Substring(0, 13);
        //    if (firstFourteen== "!!!!newName!!!!")
        //    {
        //       // message = message.Substring(14, message.Length-1);
        //        //connectedClients[]
        //       // return connectedClients.
        //    }
        //}

        private void StartServerTasks()
        {
            Task listenForClients = Task.Run(() => ListenForClients());
            Task.WaitAll();
        }








        //private void ListenForClientsAsync()
        //{
        //    Byte[] bytes = new Byte[256];
        //    String data = null;

        //    while (true)
        //    {
        //        Console.WriteLine("Waiting for a connection...");

        //        try
        //        {
        //            Client newConnection = new Client(ipAddress, port);
        //            newConnection.client =  server.AcceptTcpClient();
        //            connectedClients.Add(newConnection.Name, newConnection);


        //            Console.WriteLine("Connected!");
        //            NetworkStream stream = newConnection.client.GetStream();



        //            // Loop to receive all the data sent by the client.
        //            //listening for messages
        //            //new Task 
        //            int i;
        //            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        //            {
        //                //Encode to UTF8 (not ASCII)
        //                data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
        //                Console.WriteLine("Received: {0}", data);

        //                // Process the data sent by the client.
        //                data = data.ToUpper();
        //                byte[] msg = System.Text.Encoding.UTF8.GetBytes(data);


        //                // Send back a response.
        //                stream.Write(msg, 0, msg.Length);
        //                Console.WriteLine("Sent: {0}", data);

        //                //stream.Close();
        //                //client.Close();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }

        //    }
        //}



        //async void RunServerAsync()
        //{
        //    TcpListener listener = new TcpListener(ipAddress, port);
        //    listener.Start();

        //    try
        //    {
        //        while (true)
        //        await Accept ( await listener.AcceptTcpClientAsync());
        //    }
        //    finally
        //    {
        //        listener.Stop();
        //    }
        //}

        //async Task Accept (TcpClient client)
        //{
        //    await Task.Yield();
        //    try
        //    {
        //        using (client)
        //            using (NetworkStream stream = client.GetStream())
        //        {
        //            byte[] data = new byte[256];

        //            int bytesRead = 0;
        //            int chunkSize = 1;
        //            while (bytesRead < data.Length && chunkSize > 0)
        //                bytesRead += chunkSize =
        //                await stream.ReadAsync(data, bytesRead, data.Length - bytesRead);
        //            await stream.WriteAsync(data, 0, data.Length);
        //        }   
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}







                             //FOR OBSERVER 

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













        Queue<Message> messages = new Queue<Message>();



    }
}
