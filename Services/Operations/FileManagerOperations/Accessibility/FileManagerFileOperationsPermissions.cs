using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            else
            {
                List<NodeViewModel> selectedNodes = _treeView.SelectedNodes.OfType<NodeViewModel>().ToList();
                IEnumerable<NodeViewModel> nodesExceptLast = selectedNodes.Take(selectedNodes.Count - 1);

                if (nodesExceptLast.Any(x => x.IsFolder))
                {
                    throw new FileManagerOperationsException("Выбран каталог, не файл");
                }
                
                if (!selectedNodes.Last().IsFolder)
                {
                    throw new FileManagerOperationsException("Вместо целевого каталога выбран файл");
                }
            }
        }
    }
}
