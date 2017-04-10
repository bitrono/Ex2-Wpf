using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.Handlers;
using GameServer.Controllers.Servers;

namespace GameServer.Controllers.Servers
{
    public class TcpServer : IServer
    {
        private TcpListener listener;
        private IClientHandler clientHandler;

        public void Start(int port, IClientHandler clientHandler)
        {
            IPEndPoint ep = new
                IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");

            this.ServerTask = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        clientHandler.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            this.ServerTask.Start();
        }

        /// <summary>
        /// Wait for the server task to finish.
        /// </summary>
        public void Wait()
        {
            ServerTask.Wait();
        }

        /// <summary>
        /// Server task property
        /// </summary>
        public Task ServerTask { get; private set; }

        public void Stop()
        {
            listener.Stop();
        }
    }
}