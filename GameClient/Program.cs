using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameClient
{
    class Program
    {
        //TODO make a dictionary of commands of some sort, maybe to Game class

        static void Main(string[] args)
        {
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            IPEndPoint ep =
                new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            UserListener userListener;
            ServerListener serverListener;

            while (true)
            {
                bool continueListening = true;
                bool isMultiplayer = false;
                //Console.WriteLine("Enter a command");
                // string command = Console.ReadLine();

                TcpClient client = new TcpClient();

                client.Connect(ep);

                //Console.WriteLine("You are connected");
                //TODO dispose streams
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    serverListener = new ServerListener(client, reader);
                    serverListener.StartListening();

                    //userListener = new UserListener(client, writer);
                    // userListener.StartListening();

                    //userListener.WaitForTask();

                    while (continueListening)
                    {
                        string command = string.Empty;

                        Console.WriteLine("Enter a command");
                        command = Console.ReadLine();

                        writer.Write(command + '\n');
                        writer.Flush();

                        if (command == "start")
                        {
                            isMultiplayer = true;
                        }

                        if (!isMultiplayer || command == "close")
                        {
                            continueListening = false;
                        }
                    }


                    serverListener.Stop();
                    serverListener.WaitForTask();

                    stream.Close();
                    client.Close();
                }

                /*
                 * open task to handle user input
                 * if user opened multi
                 *     wait for user to end game 
                 */
            }
        }
    }
}


/*
 *  TcpClient client = new TcpClient();

            Console.WriteLine("Enter a command");
            string command = Console.ReadLine();

            client.Connect(ep);

            //Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                ServerListener serverListener =
                    new ServerListener(client, reader);

                serverListener.StartListening();

                writer.Write(command + '\n');
                writer.Flush();

                Task task = new Task(() =>
                {
                    while (true)
                    {
                        if (command != "start" || command != "move")
                        {
                            serverListener.Continue = true;
                            serverListener.WaitForTask();
                            stream.Close();
                            client.Close();
                            break;
                        }

                        Console.WriteLine("Enter a command");
                        command = Console.ReadLine();
                        writer.Write(command + '\n');
                        writer.Flush();
                    }
                });

                task.Start();

                task.Wait();

                //userListener = new UserListener(writer);

                //Listens for server input while the connection is open.


                //Handle user input.
                //userListener.HandleClient(client);

                //userListener.WaitForTask();
                //serverListener.Continue = false;
                //serverListener.WaitForTask();


                //stream.Close();
                //client.Close();

                //serverListener.WaitForTask();
            }

            //client.Close();
 */


/*
 *  static void Main(string[] args)
    {
        int port = int.Parse(ConfigurationManager.AppSettings["port"]);
        IPEndPoint ep =
            new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

        while (true)
        {
            TcpClient client = new TcpClient();

            client.Connect(ep);

            //Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                
                ServerListener serverListener =
                    new ServerListener(client, reader);

                //Listens for server input while the connection is open.
                serverListener.StartListening();

                //Performs single command.
                string command = Game.SingleCommand(writer);

                //If the client enters a multiplayer game.
                if (command == "start" || command == "join")
                {
                    serverListener.IsMultiplayer = true;
                    Game.EnterMultiplayer(writer, serverListener);
                }

                serverListener.WaitForTask();
            }

           client.Close();
        }
    }
 */


/*
       int port = int.Parse(ConfigurationManager.AppSettings["port"]);

       while (true)
       {
           IPEndPoint ep =
               new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
           TcpClient client = new TcpClient();
           string command;

           client.Connect(ep);

           //Console.WriteLine("You are connected");
           using (NetworkStream stream = client.GetStream())
           using (StreamReader reader = new StreamReader(stream))
           using (StreamWriter writer = new StreamWriter(stream))
           {
               Task readTask = new Task(() =>
               {
                   while (client.Connected)
                   {
                       try
                       {
                           // Get result from server
                           string result = reader.ReadLine();
                           Console.WriteLine("Result = {0}", result);
                       }
                       catch (Exception e)
                       {
                           break;
                       }
                   }
               });

               readTask.Start();

               // Send data to server
               Console.WriteLine("Enter a command");
               command = Console.ReadLine();
               writer.Write(command + '\n');
               writer.Flush();
           }

           client.Close();
       }
       */