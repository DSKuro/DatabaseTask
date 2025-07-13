using DatabaseTask.Services.Commands.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Commands
{
    public class CommandsHistory : ICommandsHistory
    {
        private Queue<ICommand> _commands = new Queue<ICommand>();

        public void AddCommand(ICommand command)
        {
            _commands.Enqueue(command);
        }

        public void RemoveCommand()
        {
            _commands.Dequeue();
        }

        public void ExecuteAllCommands()
        {
            foreach (ICommand command in _commands)
            {
                command.Execute();
            }
        }
    }
}
