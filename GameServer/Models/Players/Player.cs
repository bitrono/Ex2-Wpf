using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models.Players
{
    public class Player
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tcpClient">Socket connection</param>
        public Player(TcpClient tcpClient)
        {
            Id++;
            this.TcpClient = tcpClient;
            this.IsMultiplayer = false;
        }

        /// <summary>
        /// Client id property.
        /// </summary>
        public static int Id { get; private set; } = 0;

        /// <summary>
        /// TcpClient property
        /// </summary>
        public TcpClient TcpClient { get; set; }

        /// <summary>
        /// Is connected to a multiplayer game property.
        /// </summary>
        public bool IsMultiplayer { get; set; } //TODO delete if not needed

        /// <summary>
        /// Game room property.
        /// </summary>
        public GameRoom Room { get; set; }
    }
}