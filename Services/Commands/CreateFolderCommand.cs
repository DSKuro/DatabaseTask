using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Commands
{
    public class CreateFolderCommand : ICommand
    {
        private readonly string _folderName;
        private readonly ICreateFolderOperation _folderOperation;

        public CreateFolderCommand(ICreateFolderOperation folderOperation, string folderName)
        {
            _folderOperation = folderOperation;
            _folderName = folderName;
        }

        public void Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.CreateFolder(_folderName);
            }
        }

        public void Undo()
        {

        }
    }
}
