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
using MazeLib;

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
            string gameName = args[0];

            //TODO close both the gameRoom and delete the maze and solution on close command

            GameRoom room = this.model.Storage.Lobby.CreateNewRoom(gameName);

            if (room == null)
            {
                return ("Error: game already exists.\n");
            }

            //Creates the requested maze.
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.GenerateMaze(gameName, rows, cols);

            //Saves the maze in the started mazes storage.
            this.model.Storage.Mazes.StartedMazes.Add(maze.Name, maze);

            //Set the room maze.
            room.Maze = maze;

            room.PlayerOne = client;
            client.IsMultiplayer = true;
            client.GameRoom = room;

            //TODO note that the message appears at the client after the "Enter command", make it go before
            //Waits for the second player to join the game.
            while (!room.IsGameReady)
            {
                continue;
            }

            //Sends the maze to the client.
            string mazeInJsonFormat = maze.ToJSON();

            return mazeInJsonFormat;

            //client.StreamWriter.Write(mazeInJsonFormat);
            //client.StreamWriter.Flush();
        }
    }
}