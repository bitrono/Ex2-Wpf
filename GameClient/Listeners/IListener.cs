using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Listeners
{
    /// <summary>
    /// The interface handles listening for I/O.
    /// </summary>
    public interface IListener
    {
        /// <summary>
        /// Activates the listener for I/O.
        /// </summary>
        void StartListening();

        /// <summary>
        /// Waits for the listener to finish listening.
        /// </summary>
        void WaitForTask();

        /// <summary>
        /// Stops the listener.
        /// </summary>
        void Stop();
    }
}