using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.ItemCommands.Commands
{
    public class CopyItemCommand : ICommand
    {
        private readonly INode _copied;
        private readonly INode _target;
        private readonly string _newName;
        private readonly ICopyItemOperation _folderOperation;

        public CopyItemCommand(ICopyItemOperation folderOperation,
            INode copied,
            INode target,
            string newName)
        {
            _folderOperation = folderOperation;
            _copied = copied;
            _target = target;
            _newName = newName;
        }

        public Task Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.CopyItem(_copied, _target, _newName);
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {
            if (_folderOperation is not null)
            {
                _folderOperation.UndoCopyItem(_copied, _target, _newName);
            }
        }
    }
}
