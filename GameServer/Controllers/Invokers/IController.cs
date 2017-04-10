using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controllers.Invokers
{
    /// <summary>
    /// The basic controller interface.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// /// The execute command the controller has.
        /// </summary>
        /// <param name="commandLine">command</param>
        /// <param name="client">connection to client</param>
        /// <returns>result</returns>
        string ExecuteCommand(string commandLine, TcpClient client);
    }
}
