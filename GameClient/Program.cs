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
            IPEndPoint endPoint =
                new IPEndPoint(IPAddress.Parse(ip), port);

            TcpClient tcpClient = null;
            NetworkStream stream = null;
            StreamReader reader = null;
            StreamWriter writer = null;
            ServerListener serverListener = null;
            bool isMultiplayer = false;
            bool isConnected = false;


            //tcpClient.Connect(ep);

            while (true)
            {
                string command = string.Empty;

                Console.WriteLine("Enter a command");

                command = Console.ReadLine();

                if (!isConnected || serverListener.IsMultiplayer == false)
                {
                    tcpClient = new TcpClient();
                    //TODO dispose streams
                    tcpClient.Connect(endPoint);
                    stream = tcpClient.GetStream();
                    writer = new StreamWriter(stream);
                    reader = new StreamReader(stream);

                    isConnected = true;
                    serverListener = new ServerListener(tcpClient, reader);
                    serverListener.StartListening();
                    //start listener
                }

                try
                {
                    if (command.Split(' ')[0] == "start" ||
                        command.Split(' ')[0] == "join")
                    {
                        isMultiplayer = true;
                        serverListener.IsMultiplayer = true;
                    }

                    writer.Write(command + '\n');
                    writer.Flush();


                    if (!isMultiplayer || command == "close")
                    {
                        //TODO might need to close stream, and also dispose
                        //serverListener.Continue = false;
                        isMultiplayer = false;
                        serverListener.IsMultiplayer = false;

                        serverListener.WaitForTask();
                        stream.Close();
                        tcpClient.Close();
                        isConnected = false;
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }
    }
}


/*static void Main(string[] args)
        {
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string ip = ConfigurationManager.AppSettings["ip"];
            IPEndPoint ep =
                new IPEndPoint(IPAddress.Parse(ip), port);
            UserListener userListener;
            IListener serverListener;

            while (true)
            {
                TcpClient client = new TcpClient();

                client.Connect(ep);

                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    userListener = new UserListener(client, writer);
                    serverListener = new ServerListener(client, reader, userListener);
                    

                    //Start listening for server input.
                    serverListener.StartListening();

                    //Start listening for user input.
                    //TODO maybe open a task for userListener as well
                    userListener.StartListening();

                    //Wait for user to end connection.
                    userListener.WaitForTask();

                    //Stop listening.
                    serverListener.Stop();
                    serverListener.WaitForTask();

                    //Close connection.
                    stream.Close();
                    client.Close();
                }
            }
        }
*/