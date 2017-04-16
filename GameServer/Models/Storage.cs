using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Models.Players;

namespace GameServer.Models
{
    /// <summary>
    /// Stores containers of information.
    /// </summary>
    public class Storage
    {
        public Lobby Lobby { get; private set; }
        public Mazes Mazes { get; private set; }

        public Storage()
        {
            this.Lobby = new Lobby();
            this.Mazes = new Mazes();
        }
    }
}