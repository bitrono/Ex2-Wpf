using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Controllers.ConcreteCommands;
using GameServer.Models;

namespace GameServer.Controllers.Invokers
{
    /// <summary>
    /// The main controller of the game.
    /// Sets the commnds and executes them.
    /// </summary>
    public class MainController: IController
    {
        private Dictionary<string, ICommand> commands;
        private IList<Client> clients;
        private IModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainController()
        {
            model = new Model();
            clients = new List<Client>();
            commands = new Dictionary<string, ICommand>();
            
            //Adding commands.
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("start", new StartGameCommand(model));
            // TODO add more commands...
        }

        /// <summary>
        ///Inserts a client to the list.
        /// </summary>
        /// <param name="tcpClient"></param>
        public void InsertClient(TcpClient tcpClient)
        {
            Client newClient = new Client(tcpClient);
            clients.Add(newClient);
        }

        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];

            //Checking if command exists.
            if (!commands.ContainsKey(commandKey))
            {
                return "Command not found";
            }
            
            //Executing command.
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];

            return command.Execute(args, client);
        }

    }
}
