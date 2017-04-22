using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academy.Collections.Generic;

namespace SearchAlgorithmsLib
{

    /// <summary>
    /// Searchers that use priority queues.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class QueueSearcher<T> : ISearcher<T>
    {

        public PriorityQueue<State<T>> priorityQueue { get; set; } // Priority queue.
        public int numOfNodesEvaluted { get; set; } // Number of nodes that were evaluated.

        /// <summary>
        /// Ctor.
        /// </summary>
        public QueueSearcher()
        {
            this.priorityQueue = new PriorityQueue<State<T>>();
        }

        // Searches for the path.
        public abstract Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// Adds a state to the queue.
        /// </summary>
        /// <param name="s">The state that should be added.</param>
        protected void AddToQueue(State<T> s)
        {
            priorityQueue.Enqueue(s);
        }

        /// <summary>
        /// Gets how many nodes have been evaluated.
        /// </summary>
        /// <returns>The number of nodes evaluated.</returns>
        public int GetNumberOfNodesEvaluate()
        {
            return this.numOfNodesEvaluted;
        }

    }
}
