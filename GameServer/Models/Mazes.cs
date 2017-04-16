using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace GameServer.Models
{
    public class Mazes
    {
        public Mazes()
        {
            this.GeneratedMazes = new Dictionary<string, Maze>();
            this.SolvedMazes = new Dictionary<string, Solution<Position>>();
            this.StartedMazes = new Dictionary<string, Maze>();
        }

        public Dictionary<string, Maze> GeneratedMazes { get; private set; }

        public Dictionary<string, Solution<Position>> SolvedMazes
        {
            get;
            private set;
        }

        public Dictionary<string, Maze> StartedMazes // for multiplayer
        {
            get;
            private set;
        }


        /// <summary>
        /// Searches for a singleplayer maze in the storage.
        /// </summary>
        /// <param name="name">Maze name</param>
        /// <returns>Maze if exists, else null</returns>
        public Maze SearchGeneratedMaze(string name)
        {
            Maze maze;

            if (!GeneratedMazes.ContainsKey(name))
            {
                maze = null;
            }
            else
            {
                maze = GeneratedMazes[name];
            }

            return maze;
        }

        /// <summary>
        /// Searches for a multiplayer maze in the storage.
        /// </summary>
        /// <param name="name">Maze name</param>
        /// <returns>Maze if exists, else null</returns>
        public Maze SearchStartedMaze(string name)
        {
            Maze maze;

            if (!StartedMazes.ContainsKey(name))
            {
                maze = null;
            }
            else
            {
                maze = StartedMazes[name];
            }

            return maze;
        }
    }
}