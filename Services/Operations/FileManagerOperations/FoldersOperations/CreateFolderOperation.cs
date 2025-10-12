using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using ExCSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class CreateFolderOperation : ICreateFolderOperation
    {
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;

        public CreateFolderOperation(ITreeView treeView,
            IDataGrid dataGrid)
        {
            _treeView = treeView;
            _dataGrid = dataGrid;
        }

        public async Task CreateFolder(string folderName)
        {
            NodeViewModel node = CreateNode(folderName);
            int nodeIndex = GetNewNodeIndex(folderName);
            _treeView.SelectedNodes[0].Children.Insert(nodeIndex, node);
            CreateFolderProperties(node, nodeIndex);
            UpdateSelectedNodes(node);
            await Task.Delay(100);
            _treeView.ScrollChanged?.Invoke(this, new TreeViewEventArgs(node));
        }

        private NodeViewModel CreateNode(string folderName)
        {
            return new NodeViewModel()
            {
                Name = folderName,
                IsFolder = true,
                IconPath = IconCategory.Folder.Value,
                Parent = _treeView.SelectedNodes[0]
            };
        }

        private int GetNewNodeIndex(string folderName)
        {
            List<string> folders = _treeView.SelectedNodes[0].Children
                .Select(x => x as NodeViewModel)
                .Where(x => x != null && x.IsFolder)
                .Select(x => x!.Name)
                .ToList();
            folders.Add(folderName);
            folders.Sort();
            return folders.IndexOf(folderName);
        }

        private void UpdateSelectedNodes(NodeViewModel node)
        {
            _treeView.SelectedNodes[0].IsExpanded = true;
            _treeView.SelectedNodes.Clear();
            _treeView.SelectedNodes.Add(node);
        }

        private void CreateFolderProperties(NodeViewModel node, int nodeIndex)
        {
            int index = _dataGrid.SavedFilesProperties.FindIndex(x => x.Node == _treeView.SelectedNodes[0]);
            if (index > 0)
            {
                _dataGrid.SavedFilesProperties.Insert(index + nodeIndex, 
                    new FileProperties
                    (   
                        node.Name,
                        "",
                        _dataGrid.TimeToString(DateTime.Now),
                        IconCategory.Folder.Value, node
                    )
                );
            }
        }
    }
}
