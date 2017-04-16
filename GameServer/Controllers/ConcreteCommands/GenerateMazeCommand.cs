using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Models;
using GameServer.Views.Handlers;

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
            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
           // Maze maze = model.GenerateMaze(name, rows, cols);
           //return maze.ToJSON();

            return "maze returned...";
        }
    }
}
