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
    /// The class responsible for the command of starting a multiplayer game.
    /// </summary>
    public class StartGameCommand : ICommand
    {
        private IModel model;
        private Task task;
        private bool playerTwoConnected;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public StartGameCommand(IModel model)
        {
            this.model = model;
            this.playerTwoConnected = false;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            // task = new Task(() =>
            // {
            // while (!playerTwoConnected)
            // {

            // }
            // });

            //task.Start();

            //TODO note that the message appears at the client after the "Enter command", make it go before
            return "Waiting for second player to join...";
        }
    }
}