using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer.Controllers.Utilities
{
    /// <summary>
    /// Controlls the synchronization of the server.
    /// </summary>
    public class Mutexes
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Mutexes()
        {
            MazesMutex = new Mutex();
            LobbyMutex = new Mutex();
        }

        /// <summary>
        /// Mazes storage mutex property.
        /// </summary>
        /// <value>Mutex</value>
        public Mutex MazesMutex { get; private set; }

        /// <summary>
        /// Lobby mutex property.
        /// </summary>
        /// <value>Mutex</value>
        public Mutex LobbyMutex { get; private set; }
    }
}
