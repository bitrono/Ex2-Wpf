using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    public class GameClient
    {

        //TODO delete this class if not necessary


        public GameClient(TcpClient tcpClient)
        {
            this.TcpClient = tcpClient;
            this.IsConnected1 = false;
        }

        public TcpClient TcpClient { get; set; }

        public bool IsConnected1 { get; set; }
    }
}
