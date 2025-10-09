using DatabaseTask.Services.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;

namespace DatabaseTask.Services.Operations.FileManagerOperations.Accessibility
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

        public void CanCopyFile()
        {
            if (_treeView.SelectedNodes.Count == 0)
            {
                throw new FileManagerOperationsException("Каталог и файл не выбраны");
            }
            else if (_treeView.SelectedNodes.Count == 1)
            {
                throw new FileManagerOperationsException("Не выбран целевой каталог");
            }
            else if (_treeView.SelectedNodes.Count > 2)
            {
                throw new FileManagerOperationsException("Выбрано больше двух элементов");
            }
            else if (_treeView.SelectedNodes[0] is NodeViewModel node)
            {
                if (node.IsFolder)
                {
                    throw new FileManagerOperationsException("Выбран каталог, не файл");
                }
            }
            else if (_treeView.SelectedNodes[1] is NodeViewModel folderNode)
            {
                if (!folderNode.IsFolder)
                {
                    throw new FileManagerOperationsException("Вместо целевого каталога выбран файл");
                }
            }
        }
    }
}
