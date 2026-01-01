using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;
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

        public void CanDeleteFolder(List<INode> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new FileManagerOperationsException("Каталог не выбран");
            }
            else if (nodes.Contains(_treeView.Nodes.First())) 
            {
                throw new FileManagerOperationsException("Нельзя удалить корневой каталог");
            }
            else
            {
                foreach (NodeViewModel node in nodes)
                {

                    if (!node.IsFolder)
                    {
                        throw new FileManagerOperationsException("Выбран файл, не каталог");
                    }
                }
            }
        }

        public void CanCopyCatalog(List<INode> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new FileManagerOperationsException("Каталог не выбран");
            }
            else if (nodes.Count == 1)
            {
                throw new FileManagerOperationsException("Не выбран целевой каталог");
            }
            else if (nodes.First() == _treeView.Nodes.First())
            {
                throw new FileManagerOperationsException("Нельзя копировать корневой каталог");
            }
            else
            {
                foreach (NodeViewModel node in nodes)
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
