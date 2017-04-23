using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{

    /// <summary>
    /// Searcher objects interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// Searches for the shortest path.
        /// </summary>
        /// <param name="searchable">The searchable object.</param>
        /// <returns>The Solution of the algorithm.</returns>
        Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// Gets how many nodes have been evaluated.
        /// </summary>
        /// <returns>The number of nodes evaluated.</returns>
        int GetNumberOfNodesEvaluate();
    }
}
