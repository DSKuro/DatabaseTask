using DatabaseTask.Models;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Linq;


namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    internal class CopyItemOperation : ICopyItemOperation
    {
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;

        public CopyItemOperation(ITreeView treeView,
            IDataGrid dataGrid)
        {
            _treeView = treeView;
            _dataGrid = dataGrid;
        }

        public void CopyItem()
        {
            (INode? oldNode, INode? newNode) = AddNewNode();
            if (oldNode != null && newNode != null)
            {
                AddNewProperties(oldNode, newNode);
            }
        }

        private (INode?, INode?) AddNewNode()
        {
            INode? oldNode = _treeView.SelectedNodes.FirstOrDefault();
            INode? node = null;
            if (oldNode != null)
            {
                node = GetNewNode(oldNode);
                if (node != null)
                {
                    _treeView.SelectedNodes[1].Children.Add(node);
                }
            }

            return (oldNode, node);
        }

        private NodeViewModel? GetNewNode(INode oldNode)
        {
            if (oldNode is NodeViewModel node)
            {
                SmartCollection<INode> children = new SmartCollection<INode>();
                children.AddRange(node.Children);
                return new NodeViewModel()
                {
                    Name = node.Name,
                    IsExpanded = node.IsExpanded,
                    IsFolder = node.IsFolder,
                    IconPath = node.IconPath,
                    Parent = _treeView.SelectedNodes[1],
                    Children = children,
                };
            }
            return null;
        }

        private void AddNewProperties(INode? oldNode, INode? newNode)
        {
            FileProperties? properties = _dataGrid.SavedFilesProperties.Find(x => x.Node == oldNode);
            if (properties != null && newNode != null)
            {
                FileProperties newProperties = GetNewProperties(properties, newNode);
                int i = _dataGrid.SavedFilesProperties.FindIndex(x => x.Node == _treeView.SelectedNodes[1]);
                _dataGrid.SavedFilesProperties.Insert(i + 1, newProperties);
            }
        }

        private FileProperties GetNewProperties(FileProperties oldProperties, INode node) 
        {
            return new FileProperties(oldProperties.Name, oldProperties.Size, oldProperties.Modificated,
                oldProperties.IconPath, node);
        }
    }
}
