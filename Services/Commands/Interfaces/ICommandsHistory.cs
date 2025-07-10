namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface ICommandsHistory
    {
        public void AddCommand(ICommand command);
        public void RemoveCommand();
    }
}
