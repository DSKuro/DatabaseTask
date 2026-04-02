using DatabaseTask.Services.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.DatabaseCommands.Interfaces
{
    public interface IDatabaseCommand
    {
        public bool IsSuccess { get; }
        public Task Execute(DataContext context, List<TblDrawingContent> allRecords);
        public void Undo();
        public void Commit();
    }
}
