using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Models.Players;
using GameServer.Views.Handlers;
using GameServer.Controllers.Invokers;
using GameServer.Controllers.Servers;

namespace GameServer.Views.Handlers
{
    public class ClientHandler : IClientHandler
    {
        /// <summary>
        /// Reference to the contoller of the server.
        /// </summary>
        private readonly IController controller;

        /// <summary>
        /// The command received from the client.
        /// </summary>
        private string commandLine;

        //TODO delete these if not needed
        //private bool Continue { get; set; }
        //TODO delete these if not needed
        //private bool MultiplayerOn { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="controller"></param>
        public ClientHandler(IController controller)
        {
            this.controller = controller;
        }

        public void HandleClient(TcpClient client)
        {
            //TODO delete these if not needed
            //this.Continue = true;
            //this.MultiplayerOn = false;

            //Starts the client handler task.
            new Task(() =>
            {
                //Initializes the streams.
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    //Contains the connected client data.
                    ConnectedClient connectedClient =
                        new ConnectedClient(client, writer,
                            this.controller.GetMutexes());

                    //Loop while the client is connected.
                    while (connectedClient.IsConnected)
                    {
                        try
                        {
                            //TODO remove the Console.WriteLines if they are not needed
                            Console.WriteLine(
                                "Waiting for command");
                            commandLine = reader.ReadLine();

                            //TODO wrong way to handle find another way
                            //                        if (commandLine == null)
                            //                        {
                            //                            continue;
                            //                        }
                            Console.WriteLine("Got command: {0}", commandLine);
                            string result =
                                controller.ExecuteCommand(commandLine,
                                    connectedClient);

                            //Check if the message is not empty.
                            if (result != string.Empty)
                            {
                                Console.WriteLine(
                                    $"Sending to client: {result}");

                                //Sends message to the client.
                                writer.Write(result + '\n');
                                writer.Flush();
                            }


                            //If the client is not in a multiplayer game, close the connection.
                            if (!connectedClient.IsMultiplayer)
                            {
                                connectedClient.IsConnected = false;
                            }
                        }
                        catch (Exception e)
                        {
                            break;
                        }
                    }

                    //Close the connection.
                    stream.Close();
                    client.Close();
                }
            }).Start();
        }
    }
}