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
using SearchAlgorithmsLib;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of solving a maze.
    /// </summary>
    public class SolveMazeCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, ConnectedClient client)
        {
           //Check that all the arguments were received.
            if (args.Length != 2)
            {
                return "Error: Parameters don't match the command.\n";
            }

            string mazeName = args[0];

            //Search the requested maze in the storage.
            Maze maze = this.model.Storage.Mazes.SearchGeneratedMaze(mazeName);

            //Check that the maze exists in the storage.
            if (maze == null)
            {
                return $"Error: Maze {mazeName} doesn't exist.\n";
            }

            //Take the algorithm type.
            int algorithmType = 0;
            bool isAlgorithm = int.TryParse(args[1], out algorithmType);

            //Check that the algorithm type is a number.
            if (!isAlgorithm)
            {
                return "Error: Algorithm type must be a number.\n";
            }

            //Holds the solution in JSon format.
            string solutionInJsonFormat = string.Empty;

            //Try to solve the maze.
            try
            {
                Solution<Position> solution =
                    this.model.Solve(maze, algorithmType);
                //TODO solutionInJsonFormat = solution.ToJson();
                //TODO client.StreamWriter.Write(solutionInJsonFormat);
            }
            catch (Exception e)
            {
                return "Error: Algorithm type doesn't exist.\n";
            }

            return solutionInJsonFormat;

            //return "Solved maze...";
        }
    }
}