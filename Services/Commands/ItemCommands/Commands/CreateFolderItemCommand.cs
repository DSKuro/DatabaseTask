using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.ItemCommands.Commands
{
    public class CreateFolderItemCommand : ICommand
    {
        private readonly INode _parent;
        private readonly string _folderName;
        private readonly ICreateFolderOperation _folderOperation;

        public CreateFolderItemCommand(ICreateFolderOperation folderOperation, 
            INode parent,
            string folderName)
        {
            _folderOperation = folderOperation;
            _parent = parent;
            _folderName = folderName;
        }

        public async Task Execute()
        {
            if (_folderOperation != null)
            {
                await _folderOperation.CreateFolder(_parent, _folderName);
            }
        }

        public void Undo()
        {
            if (_folderOperation is not null)
            {
                _folderOperation.UndoCreateFolder(_parent, _folderName);
            }
        }
    }
}
