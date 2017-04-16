using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Models;
using GameServer.Models.Players;
using GameServer.Views.Handlers;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of closing a multiplayer game.
    /// </summary>
    public class CloseCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public CloseCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, ConnectedClient client)
        {
            GameRoom roomToClose = this.model.Storage.Lobby.FindGameRoom(args[0]);
            roomToClose.PlayerOne.IsMultiplayer = false;
            roomToClose.PlayerOne.Room = null;

            if (roomToClose.PlayerTwo != null)
            {
                roomToClose.PlayerTwo.IsMultiplayer = false;
                roomToClose.PlayerTwo.Room = null;
            }
          
            roomToClose.IsGameClosed = true;
            this.model.Storage.Lobby.DeleteGameRoom(args[0]);

            return "Game closed...";
        }
    }
}