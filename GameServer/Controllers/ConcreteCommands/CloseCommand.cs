using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Controllers.Servers;
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
            ConnectedClient rivalPlayer;

            //Set the players accordingly.
            if (room.PlayerOne == client)
            {
                rivalPlayer = room.PlayerTwo;
            }
            else
            {
                rivalPlayer = room.PlayerOne;
            }

            //Disconnect players from multiplayer.
            client.IsMultiplayer = false;
            rivalPlayer.IsMultiplayer = false;
            client.GameRoom = null;
            rivalPlayer.GameRoom = null;

            //Delete the maze the players played upon.
            Maze maze = room.Maze;
            this.model.Storage.Mazes.StartedMazes.Remove(maze.Name);

            //Delete game room.
            room.IsGameClosed = true;
            this.model.Storage.Lobby.DeleteGameRoom(room.Name);

            JObject emptyJObject = new JObject();

            //send empty JSon to player two to notify the game is closed.
            rivalPlayer.IsConnected = false;
            rivalPlayer.Send(emptyJObject.ToString());
            //rivalPlayer.TcpClient.GetStream().Close();
            //rivalPlayer.TcpClient.Close();
            client.IsConnected = false;

            return string.Empty;
        }
    }
}