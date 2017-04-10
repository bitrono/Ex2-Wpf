using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    public class UserListener
    {
        private StreamWriter writer;
        private TcpClient tcpClient;
        private Task task;

        public bool IsConnected { get; set; }
        public bool IsMultiplayer { get; set; }

        public UserListener(TcpClient tcpClient, StreamWriter writer)
        {
            this.tcpClient = tcpClient;
            this.writer = writer;
            this.IsMultiplayer = false;
            this.IsConnected = true;
        }

        public void StartListening()
        {
            task = new Task(() =>
            {
                while (IsConnected)
                {
                    Console.WriteLine("Enter a command");
                    string command = Console.ReadLine();
                    this.writer.Write(command + '\n');
                    this.writer.Flush();

                    if ((!this.IsMultiplayer || command != "start") ||
                        (this.IsMultiplayer && command == "close"))
                    {
                        IsConnected = false;
                    }


                    if (command == "start")
                    {
                        IsMultiplayer = true;
                    }
                }
            });

            task.Start();
        }

        public void WaitForTask()
        {
            this.task.Wait();
        }
    }
}


/*
 *  public bool IsConnected { get; set; }

        public UserListener(StreamWriter writer)
        {
            this.writer = writer;
            IsConnected = false;
        }

        public void HandleClient(TcpClient tcpClient)
        {
            if (tcpClient.Connected)
            {
                IsConnected = true;
            }

            this.task = new Task(() =>
            {
                while (IsConnected)
                {
                    // Send data to server
                    try
                    {
                        Console.WriteLine("Enter a command");
                        string command = Console.ReadLine();
                        this.writer.Write(command + '\n');
                        this.writer.Flush();

                        if (command != "start" || command != "move")
                        {
                            IsConnected = false;
                        }
                    }
                    catch (Exception e)
                    {
                        IsConnected = false;
                    }
                }
            });

            this.task.Start();
        }
 */