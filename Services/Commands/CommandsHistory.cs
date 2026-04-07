using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Database.Transaction.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.Commands
{
    public class CommandsHistory : ICommandsHistory
    {
        private readonly ICommandsTransaction _transaction;

        private Stack<ICommand> _itemCommands; 
        private Queue<IResultCommand> _commands;
        private Queue<IDatabaseCommand> _databaseCommands;

        public CommandsHistory(ICommandsTransaction transaction)
        {
            _transaction = transaction;
            _itemCommands = new Stack<ICommand>();
            _commands = new Queue<IResultCommand>();
            _databaseCommands = new Queue<IDatabaseCommand>();
        }

        public void AddItemCommand(ICommand command)
        {
            _itemCommands.Push(command);
        }

        public void AddCommand(IResultCommand command)
        {
            _commands.Enqueue(command);
        }

        public void RemoveCommand()
        {
            _commands.Dequeue();
        }

        public void AddDatabaseCommand(IDatabaseCommand command)
        {
            _databaseCommands.Enqueue(command);
        }

        public void RemoveDatabaseCommand()
        {
            _databaseCommands.Dequeue();
        }

        public void ExecuteUndoItemsCommands()
        {
            while (_itemCommands.Count > 0)
            {
                var command = _itemCommands.Pop();
                command.Undo();
            }
        }

        public List<bool> ExecuteAllCommands()
        {
            List<IResultCommand> executedCommands = new List<IResultCommand>();
            List<bool> results = ExecuteQueue(_commands, executedCommands);
            //ExecuteQueue(_databaseCommands);
            
            if (_databaseCommands.Any())
            {
                bool result = _transaction.ExecuteCommandsInTransaction(_databaseCommands);

                if (!result)
                {
                    results = results.Select(_ => false).ToList();
                }
            }

            // если всё-таки делаем отмену и для файлов, то коммитим операции
            // здесь

            CommitSuccessfulResultCommands(executedCommands, results);
            CommitSuccessfulItemCommands(results);

            return results;
        }

        private List<bool> ExecuteQueue(Queue<IResultCommand> queue, List<IResultCommand> executedCommands)
        {
            var results = new List<bool>();

            while (queue.Count > 0)
            {
                var command = queue.Dequeue();
                command.Execute();

                executedCommands.Add(command);
                results.Add(command.IsSuccess);
            }

            return results;
        }

        private void CommitSuccessfulResultCommands(List<IResultCommand> commands, List<bool> results)
        {
            for (int i = 0; i < commands.Count && i < results.Count; i++)
            {
                if (results[i])
                {
                    commands[i].Commit();
                }
            }
        }

        private void CommitSuccessfulItemCommands(List<bool> commandResults)
        {
            var itemCommandsArray = _itemCommands.ToArray();
            int n = itemCommandsArray.Length;

            for (int i = 0; i < n && i < commandResults.Count; i++) 
            {
                if (commandResults[i])
                {
                    itemCommandsArray[n - 1 - i].Commit();
                }
            }

            _itemCommands.Clear();
        }

        public void ClearAll()
        {
            _commands.Clear();
            _databaseCommands.Clear();
        }

        public int GetCommandsCount()
        {
            return _commands.Count;
        }
    }
}
