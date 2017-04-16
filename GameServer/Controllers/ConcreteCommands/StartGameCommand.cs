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
    /// The class responsible for the command of starting a multiplayer game.
    /// </summary>
    public class StartGameCommand : ICommand
    {
        private IModel model;
        private Task task;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public StartGameCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, ConnectedClient client)
        {

            client.IsMultiplayer = true;

            Player playerOne = this.model.Storage.Lobby.InsertNewPlayer(client.TcpClient);
            GameRoom room = this.model.Storage.Lobby.OpenNewRoom(args[0]);
            playerOne.Room = room;
            room.PlayerOne = playerOne;

            //TODO note that the message appears at the client after the "Enter command", make it go before
            while (!room.IsGameReady)
            {
                continue;
            }

            client.StreamWriter.Write($"{room.Name} is ready\n");
            return $"{room.Name} is ready";
        }
    }
}