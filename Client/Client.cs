﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Client
{
    public class Client    //, INotifiable
    {
        private string name;
        IPAddress ipAddress;
        public TcpClient client;
        int port = 2017;
        string formattedName;
        

        public Client(IPAddress ipAddress,int port)
        {
            name = RequestName();
            formattedName = FormatNameForServer();
            client = new TcpClient(ipAddress.ToString(), port);
            SendFormattedName();
        }

       public void SendFormattedName()
        {
            SendOneMessage(formattedName);
        }

        public void SendOneMessage(string oneMessage)
        {
            Byte[] data = System.Text.Encoding.UTF8.GetBytes(oneMessage);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", oneMessage);
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
        public string FormatNameForServer()
        {
            string formattedNameForServer;
            formattedNameForServer = "!!!!newName!!!!" + name;
            return formattedNameForServer;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public void StartClient()
        {
            SendMessage();
            ReceiveMessage();
        }


        public void SendMessage()//, IPAddress serverIPAdress)
        {
            string messageToSend = GetMessageToSend();
            AddNameToMessage(messageToSend);
            Byte[] data = System.Text.Encoding.UTF8.GetBytes(messageToSend);
           
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", messageToSend);
        }

        public string GetMessageToSend()
        {
            Console.WriteLine("Enter message:");
             string messageToSend = Console.ReadLine();
            AddNameToMessage(messageToSend);                //split methods
            return messageToSend;
        }
        public string AddNameToMessage(string message)      //TODO
        {
            message = name + ": " + message;
            return message;
        }

        public void ReceiveMessage()
        {
            string message = Console.ReadLine();
            Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            NetworkStream stream = client.GetStream();
            data = new Byte[256];
            string responseData = string.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);
            stream.Close();
            //client.Close();
        }

        public TcpClient GetConnection()
        {
            int port = 2017;
            TcpClient client = new TcpClient("192.168.0.137", port);
            return client;
        }

        private void StartClientTasks()
        {
            Task SendMessageTask = Task.Run(() => SendMessage());
            Task ReceiveMessageTask = Task.Run(() => ReceiveMessage());    
            Task.WaitAll();
        }

    }
}