using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.Invokers;

namespace GameServer.Models
{
    public class Model: IModel
    {
        public Storage Storage { get; }

        private IController controller;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="controller">Game Controller</param>
        public Model(IController controller)
        {
            this.controller = controller;
            this.Storage = new Storage();
        }
    }
}
