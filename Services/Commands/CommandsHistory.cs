using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Commands
{
    public class CommandsHistory : ICommandsHistory
    {
        private Queue<IResultCommand> _commands;
        private Queue<IResultCommand> _databaseCommands;

        public CommandsHistory()
        {
            _commands = new Queue<IResultCommand>();
            _databaseCommands = new Queue<IResultCommand>();
        }

        public void AddCommand(IResultCommand command)
        {
            _commands.Enqueue(command);
        }

        public void RemoveCommand()
        {
            _commands.Dequeue();
        }

        public void AddDatabaseCommand(IResultCommand command)
        {
            _databaseCommands.Enqueue(command);
        }

        public void RemoveDatabaseCommand()
        {
            _databaseCommands.Dequeue();
        }

        public List<bool> ExecuteAllCommands()
        {
            List<bool> results = ExecuteQueue(_commands);
            ExecuteQueue(_databaseCommands);

            return results;
        }

        private List<bool> ExecuteQueue(Queue<IResultCommand> queue)
        {
            var results = new List<bool>();

            while (queue.Count > 0)
            {
                var command = queue.Dequeue();
                command.Execute();

                results.Add(command.IsSuccess);
            }

            return results;
        }

        public void ClearAll()
        {
            _commands.Clear();
            _databaseCommands.Clear();
        }
    }
}
