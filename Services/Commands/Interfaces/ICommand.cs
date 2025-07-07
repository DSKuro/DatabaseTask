namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}
