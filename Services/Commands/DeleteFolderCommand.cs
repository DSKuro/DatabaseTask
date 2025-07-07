using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Commands
{
    public class DeleteFolderCommand : ICommand
    {
        private readonly IDeleteFolderOperation _folderOperation;

        public DeleteFolderCommand(IDeleteFolderOperation folderOperation)
        {
            _folderOperation = folderOperation;
        }

        public void Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.DeleteFolder();
            }
        }

        public void Undo()
        {

        }
    }
}
