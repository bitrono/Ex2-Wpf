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
using MazeLib;
using Newtonsoft.Json.Linq;

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
            //Get the game room members.
            GameRoom room = client.GameRoom;
            ConnectedClient playerOne;
            ConnectedClient playerTwo;

            //Set the players accordingly.
            if (client == room.PlayerOne)
            {
                playerOne = client;
                playerTwo = room.PlayerTwo;
            }
            else
            {
                playerOne = room.PlayerTwo;
                playerTwo = client;
            }

            //Disconnect players from multiplayer.
            playerOne.IsMultiplayer = false;
            playerTwo.IsMultiplayer = false;
            playerOne.GameRoom = null;
            playerTwo.GameRoom = null;

            //Delete the maze the players played upon.
            Maze maze = room.Maze;
            this.model.Storage.Mazes.StartedMazes.Remove(maze.Name);

            //Delete game room.
            room.IsGameClosed = true;
            this.model.Storage.Lobby.DeleteGameRoom(room.Name);

            JObject emptyJObject = new JObject();

            //send empty JSon to player two to notify the game is closed.
            playerTwo.StreamWriter.Write(emptyJObject.ToString() + '\n');
            playerTwo.StreamWriter.Flush();

            return string.Empty;
        }
    }
}