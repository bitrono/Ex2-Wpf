using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    static class Game
    {
        public static string SingleCommand(StreamWriter writer)
        {
            string command;

            // Send data to server
            Console.WriteLine("Enter a command");
            command = Console.ReadLine();
            writer.Write(command + '\n');
            writer.Flush();

            return command;
        }

        public static void EnterMultiplayer(StreamWriter writer, ServerListener serverListener)
        {
            //TODO try putting this in a thread
            string command = string.Empty;

            new Task(() =>
            {
                while (serverListener.Command != "close" && command != "close")
                {
                    // Send data to server
                    Console.WriteLine("Enter a command");
                    command = Console.ReadLine();
                    writer.Write(command + '\n');
                    writer.Flush();
                }
            }).Start();
           
        }
    }
}
