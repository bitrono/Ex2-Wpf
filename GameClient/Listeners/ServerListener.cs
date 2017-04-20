using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using GameClient.Listeners;

namespace GameClient
{
    public class ServerListener

    {
        private TcpClient client;
        private StreamReader reader;
        private Task readTask;
        private UserListener userListener;
        private bool isConnected;

        private bool Continue { get; set; }
        public bool IsMultiplayer { get; set; }
        private bool IsRead { get; set; }
        public string Command { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="reader"></param>
        public ServerListener(TcpClient client, StreamReader reader)
        {
            this.client = client;
            this.reader = reader;
            this.Continue = true;
            this.IsRead = false;
            this.IsMultiplayer = false;
//            this.isConnected = isConnected;
        }

        /// <summary>
        /// Listens for server input.
        /// </summary>
        public void StartListening()
        {
            Continue = true;

            this.readTask = new Task(() =>
            {
                while (Continue)
                {
                    //this.Continue = true;
                    //this.IsRead = false;

                    try
                    {
                        string rawCommand = string.Empty;
                        string readyCommand = string.Empty;

                        //Read all the data received from the server.
                        do
                        {
                            rawCommand = reader.ReadLine();

                            //Check input is not null
                            if (rawCommand != null)
                            {
                                readyCommand += rawCommand + '\n';
                            }
                        } while (reader.Peek() > 0);


                        Command = readyCommand;

                        //Check if close command was received.
                        if (Command == "{}\n")
                        {
                            Command = "Game closed";
                            //userListener.Stop();
                            IsMultiplayer = false;
                            isConnected = false;
                            client.GetStream().Close();
                            client.Close();
                        }

                        //Check if action was legal.
                        if (Command.Split(' ')[0] == "Error:")
                        {
                            Continue = false;
                            IsMultiplayer = false;
                            isConnected = false;
                            client.Close();
                        }

                        if (Command != string.Empty)
                        {
                            Console.WriteLine("Result:\n{0}", Command);
                            Command = string.Empty;
                        }
                        
                        if (!IsMultiplayer)
                        {
                            Continue = false;
                        }
                    }

                    catch (Exception e)
                    {
                        Continue = false;
                    }
                }
            });

            this.readTask.Start();
        }

        public void WaitForTask()
        {
            this.readTask.Wait();
        }

        public void Stop()
        {
            Continue = false;
        }
    }
}