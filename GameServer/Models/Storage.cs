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
        //public Solutions Solutions {get; set;} //TODO Add that

        public Storage()
        {
            this.Lobby = new Lobby();
        }
    }
}
