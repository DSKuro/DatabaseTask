namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface IFolderCommandsFactory
    {
        public ICommand CreateCreateFolderCommand(string folderName);
        public ICommand CreateRenameFolderCommand(string newName);
        public ICommand CreateDeleteFolderCommand();
    }
}
