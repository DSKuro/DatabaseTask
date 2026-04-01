using DatabaseTask.Services.Database;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.DatabaseCommands.Interfaces
{
    public interface IDatabaseCommand
    {
        public bool IsSuccess { get; }
        public Task Execute(DataContext context);
        public void Undo();
        public void Commit();
    }
}
