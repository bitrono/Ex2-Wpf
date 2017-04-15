using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models.Players
{
    public class Lobby
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Lobby()
        {
            this.Players = new List<Player>();
            this.GameRooms = new List<GameRoom>();
        }

        /// <summary>
        /// Players property.
        /// </summary>
        public IList<Player> Players { get; set; }

        /// <summary>
        /// Game rooms property.
        /// </summary>
        public IList<GameRoom> GameRooms { get; set; }

        /// <summary>
        ///Inserts a new player to the list.
        /// </summary>
        /// <param name="tcpClient">TcpClient</param>
        /// <returns>New Player</returns>
        public Player InsertNewPlayer(TcpClient tcpClient)
        {
            //TODO check if player does'nt exist already, and also remove him when he finishes the game

            Player newClient = new Player(tcpClient);

            Players.Add(newClient);

            return newClient;
        }

        /// <summary>
        /// Adds a new game room to the list.
        /// </summary>
        /// <param name="name">Game name</param>
        /// <returns>New game room</returns>
        public GameRoom OpenNewRoom(string name)
        {
            GameRoom newRoom = new GameRoom(name);

            this.GameRooms.Add(newRoom);

            return newRoom;
        }

        /// <summary>
        /// Finds the room with the given name.
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>Game room</returns>
        public GameRoom FindGameRoom(string name)
        {
            foreach (var room in this.GameRooms)
            {
                if (room.Name == name)
                {
                    return room;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes the game room with the fiven name from the list
        /// </summary>
        /// <param name="name"></param>
        public void DeleteGameRoom(string name)
        {
            foreach (var room in this.GameRooms)
            {
                if (room.Name == name)
                {
                    this.GameRooms.Remove(room);
                    break;
                }
            }
        }
    }
}