using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{

    /// <summary>
    /// Dfs Algorithm.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Dfs<T> : StackSearcher<T>
    {

        /// <summary>
        /// Ctor.
        /// </summary>
        public Dfs()
            : base()
        {
            this.numOfNodesEvaluated = 0;
        }

        /// <summary>
        /// Backtraces the shortest path and returns a list of states.
        /// </summary>
        /// <param name="finalState">The object of the final state.</param>
        /// <returns>The Solution of the algorithm.</returns>
        private Solution<T> BackTrace(State<T> finalState)
        {

            Solution<T> dfsSolution = new Solution<T>(this.numOfNodesEvaluated);
            State<T> currState = finalState;

            // Adds the entire route to nodeList.
            while (currState != null)
            {
                dfsSolution.nodeList.Add(currState);
                currState = currState.cameFrom;
            }

            return dfsSolution;
        }

        /// <summary>
        /// Searches for the shortest path using Dfs algorithm.
        /// </summary>
        /// <param name="searchable">The searchable object.</param>
        /// <returns>The Solution of the algorithm.</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            this.AddToStack(searchable.GetInitialState());
            this.numOfNodesEvaluated++;

            HashSet<State<T>> closed = new HashSet<State<T>>();

            // Continue until stack is empty.
            while (this.stack.Count() != 0)
            {

                State<T> n = this.stack.Pop(); // inherited from Searcher, removes the best state
                closed.Add(n);

                // Checks if the destination was reached.
                if (n.state.ToString().Equals(searchable.GetGoalState().state.ToString()))
                {
                    return this.BackTrace(n);
                }

                List<State<T>> successors = searchable.GetAllPossibleStates(n);

                // Iterate through all the successors and update the path.
                foreach (State<T> s in successors)
                {
                    // If not in open stack, update cameFrom and cost.
                    if (!closed.Contains(s) && !this.stack.Contains(s))
                    {
                        s.cameFrom = n; // set parent node.
                        s.cost = n.cost + 1; // update the cost of the path.
                        this.AddToStack(s);
                        this.numOfNodesEvaluated++;
                    }
                    // Else check if cost is cheaper and add to closed hash set.
                    else if (!closed.Contains(s) && this.stack.Contains(s))
                    {
                        Queue<State<T>> tempQueue = new Queue<State<T>>();
                        State<T> currState = this.stack.Pop();

                        // Find the relevant state in the stack.
                        while (!currState.Equals(s))
                        {
                            tempQueue.Enqueue(currState);
                            currState = tempQueue.Dequeue();
                        }

                        // Check if the cost of the current successor is cheaper than the cost of the
                        // same node in the stack.
                        if (s.cost < currState.cost)
                        {
                            currState = s;
                        }

                        closed.Add(this.stack.Pop());

                        // Return all the states back to the stack.
                        while (tempQueue.Count > 0)
                        {
                            this.stack.Push(currState);
                            currState = tempQueue.Dequeue();
                        }

                    }
                }
            }

            return null;
        }
    }
}
