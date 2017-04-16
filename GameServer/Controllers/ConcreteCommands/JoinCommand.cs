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
    /// The class responsible for the command of joining a player to a multiplayer game.
    /// </summary>
    public class JoinCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public JoinCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, ConnectedClient client)
        {
            GameRoom room = this.model.Storage.Lobby.FindGameRoom(args[1]);

            //Check if room exists.
            if (room == null)
            {
                return "Game does'nt exist...";
            }

            //Check if the room is available (not taken by other players).
            if (!room.IsGameAvailable)
            {
                return "Game is already taken...";
            }

            //Join the player to the room.
            Player playerTwo = this.model.Storage.Lobby.InsertNewPlayer(client);
            playerTwo.Room = room;
            room.PlayerTwo = playerTwo;
            room.IsGameReady = true;
            room.IsGameAvailable = false;

            return "Joined the game...";
        }
    }
}