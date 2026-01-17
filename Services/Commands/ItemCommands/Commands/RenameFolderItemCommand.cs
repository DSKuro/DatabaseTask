using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.ItemCommands.Commands
{
    public class RenameFolderItemCommand : ICommand
    {
        private readonly INode _node;
        private readonly string _newName;
        private readonly IRenameFolderOperation _renameOperation;

        public RenameFolderItemCommand(IRenameFolderOperation renameOperation,
            INode node,
            string newName)
        {
            _renameOperation = renameOperation;
            _node = node;
            _newName = newName;
        }

        public Task Execute()
        {
            if (_renameOperation != null)
            {
                _renameOperation.RenameFolder(_node, _newName);
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {
            if (_renameOperation != null)
            {
                _renameOperation.UndoRenameFolder(_node);
            }
        }
    }
}
