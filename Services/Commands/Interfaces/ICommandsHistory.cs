using DatabaseTask.Services.Commands.Base.Interfaces;

namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface ICommandsHistory
    {
        public void AddCommand(ICommand command);
        public void RemoveCommand();
        public void ExecuteAllCommands();
        public void ClearAll();
    }
}
