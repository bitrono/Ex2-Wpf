using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Models;
using GameServer.Views.Handlers;
using MazeLib;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of generating a maze.
    /// </summary>
    public class GenerateMazeCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, ConnectedClient client)
        {
            //Check the number of parameters received is correct.
            if (args.Length != 3)
            {
                return "Error: wrong parameters.\n";
            }

            string name = args[0];

            //Check if the maze already exists in the storage of generated mazes.
            if (this.model.Storage.Mazes.GeneratedMazes.ContainsKey(name))
            {
                return $"Error: Maze {name} already exists.\n";
            }

            //Creates the requested maze.
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.GenerateMaze(name, rows, cols);

            //Saves the maze in the storage.
            this.model.Storage.Mazes.GeneratedMazes.Add(maze.Name, maze);

            //Converts the maze to JSon format.
            string mazeInJsonFormat = maze.ToJSON();
            //client.StreamWriter.Write(mazeInJsonFormat);
            //client.StreamWriter.Flush();


            //If the client is not in a multiplayer game, close the connection.
            if (!client.IsMultiplayer)
            {
                client.IsConnected = false;
            }

            return mazeInJsonFormat;
        }
    }
}