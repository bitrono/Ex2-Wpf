using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Controllers.Servers;
using GameServer.Models.Players;
using GameServer.Models;
using GameServer.Views.Handlers;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of performing a move in the game.
    /// </summary>
    public class PlayCommand : ICommand
    {
        private IModel model;
        private IList<string> legalMoves;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public PlayCommand(IModel model)
        {
            this.model = model;
            legalMoves = new List<string>();

            //Initializes the legal moves.
            AddLegalMoves();
        }

        public string Execute(string[] args, ConnectedClient client)
        {
            //Check the number of parameters received is correct.
            if (args.Length != 1)
            {
                return "Error: wrong parameters.\n";
            }

            //Initialize game members.
            GameRoom room = client.GameRoom;
            ConnectedClient rivalPlayer;

            string move = args[0];

            //Check if the move is legal.
            if (!ContainsMove(move))
            {
                return "Error: illegal move\n";
            }

            //Set players.
            if (room.PlayerOne == client)
            {
                rivalPlayer = room.PlayerTwo;
            }
            else
            {
                rivalPlayer = room.PlayerOne;
            }

            //Sends the move to the rival.
            rivalPlayer.Send(move);

            return string.Empty;
        }

        /// <summary>
        /// Sets the legal moves into the list.
        /// </summary>
        private void AddLegalMoves()
        {
            //Adds moves.
            this.legalMoves.Add("up");
            this.legalMoves.Add("down");
            this.legalMoves.Add("left");
            this.legalMoves.Add("right");
        }

        /// <summary>
        /// Checks if the move received is a legal move.
        /// </summary>
        /// <param name="move">Desired move.</param>
        /// <returns>Is the move legal.</returns>
        private bool ContainsMove(string move)
        {
            //Search for the received move in the list.
            foreach (string legalMove in this.legalMoves)
            {
                if (legalMove == move)
                {
                    return true;
                }
            }

            return false;
        }
    }
}