namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface IFolderCommandsFactory
    {
        public ICommand CreateCreateFolderCommand(string folderName);
    }
}
