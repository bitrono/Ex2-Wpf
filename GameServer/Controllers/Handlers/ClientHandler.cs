using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.Invokers;

namespace GameServer.Controllers.Handlers
{
    public class ClientHandler : IClientHandler
    {
        private IController controller;
        private string commandLine;

        private bool Continue { get; set; }
        private bool MultiplayerOn { get; set; }

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
                    while (Continue)
                    {
                        Console.WriteLine("Waiting for command");
                        commandLine = reader.ReadLine();
                        Console.WriteLine("Got command: {0}", commandLine);
                        string result =
                            controller.ExecuteCommand(commandLine, client);
                        Console.WriteLine($"Sending to client: {result}");
                        writer.Write(result + '\n');
                        writer.Flush();

                        if (this.commandLine == "start")
                        {
                            this.MultiplayerOn = true;
                        }

                        if (!this.MultiplayerOn || this.commandLine == "close")
                        {
                            this.Continue = false;
                        }
                    }

                    stream.Close();
                    client.Close();
                }
            }).Start();
        }
    }
}