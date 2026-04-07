using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.FilesCommands.Commands
{
    public class CreateFolderCommand : IResultCommand
    {
        private readonly string _path;
        private readonly IFilesOperations _filesOperations;
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        private bool _isSuccess;

        public bool IsSuccess => _isSuccess;

        public CreateFolderCommand(
            string path,
            IFilesOperations filesOperations,
            ITreeViewFunctionality treeViewFunctionality)
        {
            _path = path;
            _filesOperations = filesOperations;
            _treeViewFunctionality = treeViewFunctionality;
            _isSuccess = false;
        }

        public Task Execute()
        {
            if (_filesOperations.CreateFolder(_path))
            {
                _isSuccess = true;
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {
            _filesOperations.DeleteFolder(_path);
        }

        public void Commit()
        {
            INode? node = _treeViewFunctionality.FindVirtualNode(Path.GetFileName(_path));
            if (node is not null)
            {
                node.FullPath = _path;
            }
        }
    }
}
