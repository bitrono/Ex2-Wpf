using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.Handlers;
using GameServer.Controllers.Invokers;
using GameServer.Controllers.Servers;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO add the View part of MVC, it should be the one receiving the messages - move all the Console.WriteLine() to the view

            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            IController controller = new MainController();
            IClientHandler clientHandler = new ClientHandler(controller);
            IServer server = new TcpServer();

            server.Start(port, clientHandler);
            server.Wait();


        }
    }
}
