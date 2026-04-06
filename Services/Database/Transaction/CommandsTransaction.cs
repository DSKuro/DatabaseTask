using DatabaseTask.Models.AppData;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Database.Transaction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.Database.Transaction
{
    public class CommandsTransaction : ICommandsTransaction
    {
        private readonly ConnectionStringData _stringData;
        private readonly ITblDrawingContentsRepository _drawingRepository;

        public CommandsTransaction(ConnectionStringData stringData,
                                   ITblDrawingContentsRepository drawingRepository)
        {
            _stringData = stringData;
            _drawingRepository = drawingRepository;
        }

        public bool ExecuteCommandsInTransaction(Queue<IDatabaseCommand> commands)
        {
            using var context = new DataContext(_stringData.ConnectionString);
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var commandList = commands.ToList();

                var paths = commandList
                    .Select(x => x.SourcePath)
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var pathIndex = _drawingRepository.GetPathIndex(context, paths);

                ExecuteQueue(commands, context, pathIndex);
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (System.Exception)
            {
                transaction.Rollback();
                return false;
            }
        }

        private List<bool> ExecuteQueue(Queue<IDatabaseCommand> queue, DataContext context,
             Dictionary<string, List<TblDrawingContent>> pathIndex)
        {
            var results = new List<bool>();

            while (queue.Count > 0)
            {
                var command = queue.Dequeue();
                command.Execute(context, pathIndex);

                results.Add(command.IsSuccess);
            }

            return results;
        }
    }
}
