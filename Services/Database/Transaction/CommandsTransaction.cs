using DatabaseTask.Models.AppData;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Database.Transaction.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Database.Transaction
{
    public class CommandsTransaction : ICommandsTransaction
    {
        private readonly ConnectionStringData _stringData;

        public CommandsTransaction(ConnectionStringData stringData)
        {
            _stringData = stringData;
        }

        public void ExecuteCommandsInTransaction(Queue<IDatabaseCommand> commands)
        {
            using var context = new DataContext(_stringData.ConnectionString);
            using var transaction = context.Database.BeginTransaction();
            try
            {
                ExecuteQueue(commands, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
            }
        }

        private List<bool> ExecuteQueue(Queue<IDatabaseCommand> queue, DataContext context)
        {
            var results = new List<bool>();

            while (queue.Count > 0)
            {
                var command = queue.Dequeue();
                command.Execute(context);

                results.Add(command.IsSuccess);
            }

            return results;
        }
    }
}
