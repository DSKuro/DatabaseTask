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
        private readonly ICopyItemOperation _folderOperation;

        public CopyItemCommand(INode copied,
            INode target,
            ICopyItemOperation folderOperation)
        {
            _copied = copied;
            _target = target;
            _folderOperation = folderOperation;
        }

        public Task Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.CopyItem(_copied, _target);
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {


        }
    }
}
