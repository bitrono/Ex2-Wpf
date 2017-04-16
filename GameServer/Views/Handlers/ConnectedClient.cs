using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Models.Players;

namespace GameServer.Views.Handlers
{
    public class ConnectedClient
    {
       public ConnectedClient(TcpClient tcpClient, StreamWriter streamWriter)
        {
            this.TcpClient = tcpClient;
            this.StreamWriter = streamWriter;
            this.IsMultiplayer = false;
            this.IsConnected = true;
            this.GameRoom = null;
        }

        public TcpClient TcpClient { get; private set; }
        public StreamWriter StreamWriter { get; private set; }
        public bool IsMultiplayer { get; set; }
        public bool IsConnected { get; set; }
        public GameRoom GameRoom { get; set; }
    }
}
