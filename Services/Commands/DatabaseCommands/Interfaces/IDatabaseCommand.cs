using DatabaseTask.Services.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.DatabaseCommands.Interfaces
{
    public interface IDatabaseCommand
    {
        public bool IsSuccess { get; }
        public string SourcePath { get; }
        public Task Execute(DataContext context, Dictionary<string, List<TblDrawingContent>> pathIndex);
        public void Undo();
        public void Commit();
    }
}
