using DatabaseTask.Services.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.FileManagerOperations.Exceptions;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System.Linq;

namespace DatabaseTask.Services.FileManagerOperations.Accessibility
{
    public class FileManagerFolderOperationsPermissions : IFileManagerFolderOperationsPermissions
    {
        private readonly ITreeView _treeView;

        public FileManagerFolderOperationsPermissions(ITreeView treeView)
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

        public void CanDeleteFolder()
        {
            if (_treeView.SelectedNodes.Count == 0)
            {
                throw new FileManagerOperationsException("Каталог не выбран");
            }
            else if (_treeView.SelectedNodes.Contains(_treeView.Nodes.First())) 
            {
                throw new FileManagerOperationsException("Нельзя удалить корневой каталог");
            }
            else
            {
                foreach (NodeViewModel node in _treeView.SelectedNodes)
                {

                    if (!node.IsFolder)
                    {
                        throw new FileManagerOperationsException("Выбран файл, не каталог");
                    }
                }
            }
        }
    }
}
