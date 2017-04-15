using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameClient.Listeners;
using Newtonsoft.Json;

namespace GameClient
{
    class Program
    {
        //TODO make a dictionary of commands of some sort, maybe to Game class

        static void Main(string[] args)
        {
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string ip = ConfigurationManager.AppSettings["ip"];
            IPEndPoint ep =
                new IPEndPoint(IPAddress.Parse(ip), port);
            IListener userListener;
            IListener serverListener;

            while (true)
            {
                TcpClient client = new TcpClient();

                client.Connect(ep);

                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    serverListener = new ServerListener(client, reader);
                    userListener = new UserListener(client, writer);

                    //Start listening for server input.
                    serverListener.StartListening();

                    //Start listening for user input.
                    //TODO maybe open a task for userListener as well
                    userListener.StartListening();

                    //Stop listening
                    serverListener.Stop();
                    serverListener.WaitForTask();

                    //Close connection.
                    stream.Close();
                    client.Close();
                }
            }
        }
    }
}