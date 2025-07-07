using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Commands
{
    public class RenameFolderCommand : ICommand
    {
        private readonly string _newName;
        private readonly IRenameFolderOperation _renameOperation;

        public RenameFolderCommand(IRenameFolderOperation renameOperation, string newName)
        {
            _renameOperation = renameOperation;
            _newName = newName;
        }

        public void Execute()
        {
            if (_renameOperation != null)
            {
                _renameOperation.RenameFolder(_newName);
            }
        }

        public void Undo()
        {

        }
    }
}
