using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Controllers.Utilities;
using GameServer.Models;
using GameServer.Models.Players;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of returning the availiable mazes.
    /// </summary>
    public class ListCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public ListCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null)
        {
            string listOfGames = "Rooms:";
            string parsedList = string.Empty;

            IList<GameRoom> gameRooms = this.model.Storage.Lobby.GameRooms;

            foreach (GameRoom room in gameRooms)
            {
                if (room.IsGameAvailable)
                {
                    listOfGames += "\n";
                    listOfGames += room.Name;
                }
            }

            parsedList = Parser.ChangeNewLine(listOfGames);

            return parsedList;
        }
    }
}