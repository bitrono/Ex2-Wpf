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
    public class StartGameCommand: ICommand
    {
        private IModel model;
        private Task task;
        private bool playerTwoConnected;

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

            return "Waiting for second player to join...";
        }
    }
}
