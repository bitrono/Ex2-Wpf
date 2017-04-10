using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controllers
{
    public class Client
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tcpClient">Socket connection</param>
        public Client(TcpClient tcpClient)
        {
            Id++;
            this.TcpClient = tcpClient;
        }

        /// <summary>
        /// Client id property.
        /// </summary>
        public static int Id { get; private set; } = 0;

        /// <summary>
        /// TcpClient property
        /// </summary>
        public TcpClient TcpClient { get; set; }
    }
}
