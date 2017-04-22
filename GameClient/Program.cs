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
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Command line args.</param>
        static void Main(string[] args)
        {
            //End point initialization.
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string ip = ConfigurationManager.AppSettings["ip"];
            IPEndPoint endPoint =
                new IPEndPoint(IPAddress.Parse(ip), port);

            //Vatiables declaration.
            TcpClient tcpClient = null;
            NetworkStream stream = null;
            StreamReader reader = null;
            StreamWriter writer = null;
            IListener serverListener = null;
            bool isMultiplayer = false;
            bool isConnected = false;

            //Loop while client is on.
            while (true)
            {
                string command = string.Empty;

                Console.WriteLine("Enter a command");

                command = Console.ReadLine();

                //If not connected, Initialize connection.
                if (!isConnected || serverListener.IsMultiplayer == false)
                {
                    tcpClient = new TcpClient();
                    tcpClient.Connect(endPoint);
                    stream = tcpClient.GetStream();
                    writer = new StreamWriter(stream);
                    reader = new StreamReader(stream);
                    isConnected = true;
                    serverListener = new ServerListener(tcpClient, reader);

                    //start listener
                    serverListener.StartListening();
                }

                //Communicate with server.
                try
                {
                    //Check if the connection needs to remain open.
                    if (command.Split(' ')[0] == "start" ||
                        command.Split(' ')[0] == "join")
                    {
                        isMultiplayer = true;
                        serverListener.IsMultiplayer = true;
                    }

                    //Send message to server.
                    writer.Write(command + '\n');
                    writer.Flush();

                    //Check if connection can be closed.
                    if (!isMultiplayer || command == "close")
                    {
                        isMultiplayer = false;
                        serverListener.IsMultiplayer = false;

                        //Wait for listener to end.
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