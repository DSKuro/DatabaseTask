using DatabaseTask.Services.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.FileManagerOperations.Exceptions;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;

namespace DatabaseTask.Services.FileManagerOperations.Accessibility
{ 
    public class FileManagerFileOperationsPermissions : IFileManagerFileOperationsPermissions
    {
        private readonly ITreeView _treeView;

        public FileManagerFileOperationsPermissions(ITreeView treeView)
        {
            _treeView = treeView;
        }

        public void CanDeleteFile()
        {
            if (_treeView.SelectedNodes.Count == 0)
            {
                throw new FileManagerOperationsException("Файл не выбран");
            }
            else
            {
                foreach (NodeViewModel node in _treeView.SelectedNodes)
                {

                    if (node.IsFolder)
                    {
                        throw new FileManagerOperationsException("Выбран каталог, не файл");
                    }
                }
            }
        }
    }
}
