using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Models.Players;
using GameServer.Models;
using GameServer.Views.Handlers;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of performing a move in the game.
    /// </summary>
    public class PlayCommand : ICommand
    {
        private IModel model;
    
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public PlayCommand(IModel model)
        {
            this.model = model;
      }

        public string Execute(string[] args, ConnectedClient client)
        {
            return "Moved...";
        }
    }
}