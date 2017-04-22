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

        // The search method. 
        Solution<T> Search(ISearchable<T> searchable);

        // Get how many nodes were evaluated by algorithm.
        int GetNumberOfNodesEvaluate();
    }
}
