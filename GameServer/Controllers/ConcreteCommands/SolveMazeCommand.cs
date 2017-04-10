using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Models;

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

        public string Execute(string[] args, TcpClient client = null)
        {
           
            return "Solved maze...";
        }
    }
}