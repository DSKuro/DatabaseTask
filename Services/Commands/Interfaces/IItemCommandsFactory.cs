namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface IItemCommandsFactory
    {
        public ICommand CreateCreateFolderCommand(string folderName);
        public ICommand CreateRenameFolderCommand(string newName);
        public ICommand CreateDeleteItemCommand();
    }
}
