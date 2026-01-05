namespace DatabaseTask.Services.Commands.Base.Interfaces
{
    public interface IResultCommand : ICommand
    {
        public bool IsSuccess { get; }
    }
}
