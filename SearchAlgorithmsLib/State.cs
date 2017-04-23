using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{

    /// <summary>
    /// The state of a searchable object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T> : IComparable
    {
        /// <summary>
        /// The state represented by a string.
        /// </summary>
        public T state { get; set; }

        /// <summary>
        /// The cost to reach this state (set by a setter).
        /// </summary>
        public double cost { get; set; }

        /// <summary>
        /// The state we came from to this state (setter).
        /// </summary>
        public State<T> cameFrom { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="state">The state.</param>
        public State(T state)
        {
            this.state = state;
            this.cost = 0;
            this.cameFrom = null;
        }

        /// <summary>
        /// Checks if state is equal to another state.
        /// </summary>
        /// <param name="s">State to compare with.</param>
        /// <returns>Boolean if state is equal.</returns>
        public bool Equals(State<T> s)
        {
            return state.Equals(s.state);
        }

        /// <summary>
        /// Compares this object with other object.
        /// </summary>
        /// <param name="obj">Other object.</param>
        /// <returns>Whether states are equal, greater or less than.</returns>
        public int CompareTo(object obj)
        {

            State<T> otherState = obj as State<T>;
            if (this.cost > otherState.cost) return -1;
            if (this.cost == otherState.cost) return 0;
            return 1;
        }

    }
}
