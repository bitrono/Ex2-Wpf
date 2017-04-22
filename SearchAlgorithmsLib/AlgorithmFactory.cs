using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{

    /// <summary>
    /// Factory design pattern. Creates Algorithm objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AlgorithmFactory<T>
    {

        /// <summary>
        /// Returns the relevant algorithm object.
        /// </summary>
        /// <param name="algorithm">Id of the relevant algorithm.</param>
        /// <returns>The algorithm object.</returns>
        public ISearcher<T> CreateAlgorithm(int algorithm)
        {

            // Finds which specific algorithm to create.
            switch (algorithm)
            {
                case 0:
                    return new Bfs<T>();

                case 1:
                    return new Dfs<T>();
            }

            return null;
        }
    }
}
