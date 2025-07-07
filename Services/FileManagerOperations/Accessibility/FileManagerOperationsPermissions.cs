using DatabaseTask.Services.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.FileManagerOperations.Exceptions;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;

namespace DatabaseTask.Services.FileManagerOperations.Accessibility
{
    public class FileManagerOperationsPermissions : IFileManagerOperationsPermissions
    {
        private readonly ITreeView _treeView;

        public FileManagerOperationsPermissions(ITreeView treeView)
        {
            _treeView = treeView;
        }

        public void CanDoOperationOnFolder() 
        {
            if (_treeView.SelectedNodes.Count == 0)
            {
                throw new FileManagerOperationsException("Каталог не выбран");
            }
            else if (_treeView.SelectedNodes.Count > 1)
            {
                throw new FileManagerOperationsException("Выбрано больше одного каталога");
            }
            else if (_treeView.SelectedNodes[0] is NodeViewModel node)
            {
                if (!node.IsFolder)
                {
                    throw new FileManagerOperationsException("Выбран файл, не каталог");
                }
            }
        }
    }
}
