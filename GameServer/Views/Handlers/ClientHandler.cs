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

namespace GameServer.Views.Handlers
{
    public class ClientHandler : IClientHandler
    {
        private IController controller;
        private string commandLine;

        //TODO delete these if not needed
        private bool Continue { get; set; }

        private bool MultiplayerOn { get; set; }

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
            this.Continue = true;
            this.MultiplayerOn = false;
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    //Contains the connected client data.
                    ConnectedClient connectedClient =
                        new ConnectedClient(client, writer);

                    //Loop while the client is connected.
                    while (connectedClient.IsConnected)
                    {
                        //TODO remove the Console.WriteLines if they are not needed
                        Console.WriteLine(
                            "Waiting for command"); 
                        commandLine = reader.ReadLine();
                        Console.WriteLine("Got command: {0}", commandLine);
                        string result =
                            controller.ExecuteCommand(commandLine,
                                connectedClient);

                        //Check if the message is not empty.
                        if (result != string.Empty)
                        {
                            Console.WriteLine($"Sending to client: {result}");
                            
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

                    //Close the connection.
                    stream.Close();
                    client.Close();
                }
            }).Start();
        }
    }
}