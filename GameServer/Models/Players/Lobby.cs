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
            //TODO delete players if not needed. or rename ConnectedClient to Players
            this.Players = new List<Player>();
            this.GameRooms =new Dictionary<string, GameRoom>();
        }

        /// <summary>
        /// Players property.
        /// </summary>
        /// TODO delete if not needed
        public IList<Player> Players { get; set; }

        /// <summary>
        /// Game rooms property.
        /// </summary>
        public Dictionary<string, GameRoom> GameRooms { get; private set; }

        /// <summary>
        ///Inserts a new player to the list.
        /// </summary>
        /// <param name="tcpClient">TcpClient</param>
        /// <returns>New Player</returns>
        public Player InsertNewPlayer(TcpClient tcpClient)
        {
            //TODO delete this method if not needed
            //TODO check if player does'nt exist already, and also remove him when he finishes the game

            Player newClient = new Player(tcpClient);

            Players.Add(newClient);

            return newClient;
        }

        /// <summary>
        /// Adds a new game room to the storage.
        /// </summary>
        /// <param name="name">Game name</param>
        /// <returns>New game room</returns>
        public GameRoom CreateNewRoom(string name)
        {

            if (this.GameRooms.ContainsKey(name))
            {
                return null;
            }

            GameRoom newRoom = new GameRoom(name);
            
            this.GameRooms.Add(name, newRoom);

            return newRoom;
        }

        /// <summary>
        /// Finds the room with the given name.
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>Game room</returns>
        public GameRoom SearchGameRoom(string name)
        {
            GameRoom gameRoom;

            //Check if game room exists in storage.
            if (!GameRooms.ContainsKey(name))
            {
                return null;
            }

            //Sets the return value to requested game room.
            gameRoom = GameRooms[name];

            return gameRoom;

        }

        /// <summary>
        /// Removes the game room with the fiven name from the list
        /// </summary>
        /// <param name="name"></param>
        public void DeleteGameRoom(string name)
        {
            //Check if game room exists in storage.
            if (!GameRooms.ContainsKey(name))
            {
                return;
            }

            //Removes the game room from storage.
            GameRooms.Remove(name);
        }
    }
}