using DatabaseTask.Models.AppData;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Database.Transaction.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
                var allRecords = context.TblDrawingContents
                      .Where(x => !string.IsNullOrEmpty(x.ContentDocument))
                      .ToList();

                var commandList = commands.ToList();
                var pathIndex = BuildPathIndex(allRecords, commandList);

                ExecuteQueue(commands, context, pathIndex);
                context.SaveChanges();
            }
            catch (System.Exception)
            {
                transaction.Rollback();
            }
        }

        private Dictionary<string, List<TblDrawingContent>> BuildPathIndex(
    List<TblDrawingContent> allRecords,
    List<IDatabaseCommand> commands)
        {
            var paths = commands
                .Select(x => x.SourcePath)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var index = new Dictionary<string, List<TblDrawingContent>>(
                StringComparer.OrdinalIgnoreCase);

            foreach (var path in paths)
            {
                var relativePath = path.StartsWith(@".\")
                    ? path[2..]
                    : path;

                index[path] = allRecords
                    .Where(r =>
                        r.ContentDocument!.Contains(path,
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        r.ContentDocument.Contains(
                            $@"\dwg\{relativePath}",
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return index;
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
