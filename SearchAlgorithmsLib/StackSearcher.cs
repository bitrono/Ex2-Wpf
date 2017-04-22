using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{

    /// <summary>
    /// Searchers that use a stack.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StackSearcher<T> : ISearcher<T>
    {

        public Stack<State<T>> stack { get; set; }
        public int numOfNodesEvaluated { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        protected StackSearcher()
        {
            this.stack = new Stack<State<T>>();
        }

        // Searches for the path.
        public abstract Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// Adds a state to the stack.
        /// </summary>
        /// <param name="s">The state that needs to be added to the stack.</param>
        protected void AddToStack(State<T> s)
        {
            stack.Push(s);
        }

        /// <summary>
        /// Gets number of nodes that have been evaluated.
        /// </summary>
        /// <returns>The number of nodes evaluated.</returns>
        public int GetNumberOfNodesEvaluate()
        {
            return this.numOfNodesEvaluated;
        }

    }
}
