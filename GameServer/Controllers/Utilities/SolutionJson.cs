using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controllers.Utilities
{

    /// <summary>
    /// Solution that assists with json format.
    /// </summary>
    public class SolutionJson
    {
        public string name { get; set; }
        public string solution { get; set; }
        public string evalNodes { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="name">Name of the maze.</param>
        /// <param name="solution">The solution.</param>
        /// <param name="evalNodes">The number of nodes evaluated.</param>
        public SolutionJson(string name, string solution, string evalNodes)
        {
            this.name = name;
            this.solution = solution;
            this.evalNodes = evalNodes;
        }
    }
}
