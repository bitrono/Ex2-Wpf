using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{

    /// <summary>
    /// Searchable objects interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Returns the inital state.
        /// </summary>
        /// <returns>position of initial state.</returns>
        State<T> GetInitialState();

        /// <summary>
        /// Returns the goal state.
        /// </summary>
        /// <returns>position of goal state.</returns>
        State<T> GetGoalState();

        /// <summary>
        /// Returns all the possible states.
        /// </summary>
        /// <param name="s">Position current state.</param>
        /// <returns>States relative to s.</returns>
        List<State<T>> GetAllPossibleStates(State<T> s);

    }
}
