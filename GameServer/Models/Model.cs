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
    /// <summary>
    /// Contains the server's logic and data.
    /// </summary>
    public class Model : IModel
    {
        /// <summary>
        /// Reference to the server's cotroller.
        /// </summary>
        private IController controller;

        /// <summary>
        /// A maze generator.
        /// </summary>
        private IMazeGenerator mazeGenerator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="controller">Game Controller</param>
        public Model(IController controller)
        {
            this.controller = controller;
            this.Storage = new Storage();
            this.AlgorithmFac = new AlgorithmFactory<Position>();
            this.mazeGenerator = new DFSMazeGenerator();
        }

        /// <summary>
        /// Storage property.
        /// </summary>
        /// <value>Sorage.</value>
        public Storage Storage { get; }

        /// <summary>
        /// Algorithm factory property.
        /// </summary>
        /// <value>AlgorithmFactory.</value>
        public AlgorithmFactory<Position>
            AlgorithmFac { get; }

        public Solution<Position> Solve(Maze maze, int algorithmId)
        {
            //Solves the maze.
            StatePool<Position> sp = new StatePool<Position>();

            //Solution adapter.
            Adapter ad = new Adapter(maze.Rows, maze.Cols, maze.InitialPos.Row,
                maze.InitialPos.Col, maze.GoalPos.Row, maze.GoalPos.Col,
                maze.Name, sp);

            //Generates an algorithm to solve the maze.
            ISearcher<Position> algorithm =
                this.AlgorithmFac.CreateAlgorithm(algorithmId);

            //Returns a maze solution.
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