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
                    ConnectedClient connectedClient = new ConnectedClient(client, writer);

                    while (connectedClient.IsConnected)
                    {
                        //TODO remove the Console.WriteLines if they are not needed
                        Console.WriteLine("Waiting for command");            //what happens during play from other player, how to get it? and what is the usage of the tcpClient in the command?
                        commandLine = reader.ReadLine();
                        Console.WriteLine("Got command: {0}", commandLine);
                        controller.ExecuteCommand(commandLine, client);

                        /*
                        string result =
                            controller.ExecuteCommand(commandLine, client);
                        Console.WriteLine($"Sending to client: {result}");
                        writer.Write(result + '\n');
                        writer.Flush();

                        
                        //TODO remove that
                        if (this.commandLine.Split(' ')[0] == "start")
                        {
                            this.MultiplayerOn = true;
                        }

                        if (!this.MultiplayerOn || this.commandLine == "close")
                        {
                            this.Continue = false;
                        }
                        */
                    }

                    stream.Close();
                    client.Close();
                }
            }).Start();
        }
    }
}