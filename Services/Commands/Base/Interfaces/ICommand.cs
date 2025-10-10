using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.Base.Interfaces
{
    public interface ICommand
    {
        public Task Execute();
        public void Undo();
    }
}
