using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models
{
    /// <summary>
    /// The basic model interface.
    /// </summary>
    public interface IModel
    {
        // Maze GenerateMaze(string name, int rows, int columns)

        /// <summary>
        /// Storage getter.
        /// </summary>
        Storage Storage { get; }
    }
}
