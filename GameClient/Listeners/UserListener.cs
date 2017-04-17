using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameClient.Listeners;

namespace GameClient
{
    public class UserListener : IListener
    {
        private StreamWriter writer;
        private TcpClient tcpClient;
        private Task writeTask;

        public bool ContinueListening { get; set; }
        public bool IsMultiplayer { get; set; }

        public UserListener(TcpClient tcpClient, StreamWriter writer)
        {
            this.tcpClient = tcpClient;
            this.writer = writer;
            this.IsMultiplayer = false;
            this.ContinueListening = true;
        }

        public void StartListening()
        {
            this.writeTask = new Task(() =>
            {
                while (ContinueListening)
                {
                    string command = string.Empty;

                    Console.WriteLine("Enter a command\n");
                    command = Console.ReadLine();

                    writer.Write(command + '\n');
                    writer.Flush();

                    if (command.Split(' ')[0] == "start" || command.Split(' ')[0] == "join")
                    {
                        IsMultiplayer = true;
                    }

                    if (!IsMultiplayer || command == "close")
                    {
                        ContinueListening = false;
                    }
                }
            });

            this.writeTask.Start();
        }

        public void WaitForTask()
        {
            this.writeTask.Wait();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
