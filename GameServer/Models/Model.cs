using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.Adapters;
using GameServer.Controllers.Invokers;
using GameServer.Controllers.Utilities;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace GameServer.Models
{
    public class Model : IModel
    {
        private IController controller;
        private IMazeGenerator mazeGenerator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="controller">Game Controller</param>
        public Model(IController controller)
        {
            this.controller = controller;
            this.Storage = new Storage();
            this.algorithmFac = new AlgorithmFactory<Position>();
            this.mazeGenerator = new DFSMazeGenerator();
        }

        public Storage Storage { get; }

        public AlgorithmFactory<Position>
            algorithmFac { get; } // Algorithm Factory.

        public Solution<Position> Solve(Maze maze, int algorithmId)
        {
            StatePool<Position> sp = new StatePool<Position>();
            Adapter ad = new Adapter(maze.Rows, maze.Cols, maze.InitialPos.Row,
                maze.InitialPos.Col, maze.GoalPos.Row, maze.GoalPos.Col,
                maze.Name, sp);
            ISearcher<Position> algorithm =
                this.algorithmFac.CreateAlgorithm(algorithmId);
            return algorithm.search(ad);
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            //Generates the maze with the required properties.
            Maze maze = this.mazeGenerator.Generate(rows, cols);
            maze.Name = name;

            return maze;
        }
    }
}