using DatabaseTask.Services.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Linq;

namespace DatabaseTask.Services.Operations.FileManagerOperations.Accessibility
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

        public void CanCopyCatalog()
        {
            if (_treeView.SelectedNodes.Count == 0)
            {
                throw new FileManagerOperationsException("Каталог не выбран");
            }
            else if (_treeView.SelectedNodes.Count == 1)
            {
                throw new FileManagerOperationsException("Не выбран целевой каталог");
            }
            else if (_treeView.SelectedNodes.Count > 2)
            {
                throw new FileManagerOperationsException("Выбрано больше двух каталогов");
            }
            else if (_treeView.SelectedNodes.First() == _treeView.Nodes.First())
            {
                throw new FileManagerOperationsException("Нельзя копировать корневой каталог");
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
