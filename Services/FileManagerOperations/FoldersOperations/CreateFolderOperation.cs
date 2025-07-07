using DatabaseTask.Services._serviceCollection;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.EventArguments;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using ExCSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.FileManagerOperations.FoldersOperations
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
            List<string> folders = _treeView.SelectedNodes[0].Children.Where(x => x is NodeViewModel)
                .Where(x => (x as NodeViewModel).IsFolder)
                .Select(x => (x as NodeViewModel).Name).ToList();
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
            _dataGrid.SavedFilesProperties.Insert(index + nodeIndex, new Models.FileProperties
                (node.Name, 
                "",
                _dataGrid.TimeToString(DateTime.Now),
                IconCategory.Folder.Value, node));
        }
    }
}
