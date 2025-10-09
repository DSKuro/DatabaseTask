using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Commands.ItemCommands.Commands
{
    public class RenameFolderItemCommand : ICommand
    {
        private readonly string _newName;
        private readonly IRenameFolderOperation _renameOperation;

        public RenameFolderItemCommand(IRenameFolderOperation renameOperation,
            string newName)
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
