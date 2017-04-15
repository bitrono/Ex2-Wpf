using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Controllers.Utilities
{
    /// <summary>
    /// Provides parsing services.
    /// </summary>
    static class Parser
    {
        /// <summary>
        /// Replaces all the new line chars with a special char.
        /// </summary>
        /// <param name="input">initial string</param>
        /// <returns>parsed string</returns>
        public static string ChangeNewLine(string input)
        {
            //Replace all instances of '\n' with '@'.
            return input.Replace('\n', '@');
        }
    }
}
