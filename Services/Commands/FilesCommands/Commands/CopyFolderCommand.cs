using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.FilesCommands.Commands
{
    public class CopyFolderCommand : IResultCommand
    {
        private readonly string _oldPath;
        private readonly string _newPath;
        private readonly IFilesOperations _filesOperations;
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        private bool _isSuccess;

        public bool IsSuccess => _isSuccess;

        public CopyFolderCommand(
            string oldPath,
            string newPath,
            IFilesOperations filesOperations,
            ITreeViewFunctionality treeViewFunctionality)
        {
            _oldPath = oldPath;
            _newPath = newPath;
            _filesOperations = filesOperations;
            _treeViewFunctionality = treeViewFunctionality;
            _isSuccess = false;
        }

        public Task Execute()
        {
            if (_filesOperations.CopyFolder(_oldPath, _newPath))
            {
                _isSuccess = true;
            }
            return Task.CompletedTask;
        }

        public void Undo()
        {
            _filesOperations.DeleteFolder(_newPath);
        }

        public void Commit()
        {
            INode? node = _treeViewFunctionality.FindVirtualNode(Path.GetFileName(_newPath));
            if (node is null)
            {
                return;
            }

            UpdatePathRecursive(node, _newPath);
        }

        private static void UpdatePathRecursive(INode node, string path)
        {
            node.FullPath = path;

            foreach (INode child in node.Children)
            {
                if (child.Name == "Loading...")
                {
                    continue;
                }

                UpdatePathRecursive(child, Path.Combine(path, child.Name));
            }
        }
    }
}
