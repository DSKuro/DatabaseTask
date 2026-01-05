using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Commands
{
    public class CommandsHistory : ICommandsHistory
    {
        private Queue<IResultCommand> _commands;

        public CommandsHistory()
        {
            _commands = new Queue<IResultCommand>();
        }

        public void AddCommand(IResultCommand command)
        {
            _commands.Enqueue(command);
        }

        public void RemoveCommand()
        {
            _commands.Dequeue();
        }

        public List<bool> ExecuteAllCommands()
        {
            List<bool> results = new List<bool>();
            foreach (IResultCommand command in _commands)
            {
                command.Execute();
                results.Add(command.IsSuccess);
            }
            ClearAll();
            return results;
        }

        public void ClearAll()
        {
            _commands.Clear();
        }
    }
}
