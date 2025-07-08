using DatabaseTask.Models;
using DatabaseTask.Services._serviceCollection;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System.Linq;


namespace DatabaseTask.Services.FileManagerOperations.FoldersOperations
{
    internal class CopyFolderOperation : ICopyFolderOperation
    {
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;

        public CopyFolderOperation(ITreeView treeView,
            IDataGrid dataGrid)
        {
            _treeView = treeView;
            _dataGrid = dataGrid;
        }

        public void CopyFolder(bool IsCopy)
        {
            (INode oldNode, INode newNode) = AddNewNode();
            FileProperties properties = AddNewProperties(oldNode, newNode);
            if (!IsCopy)
            {
                oldNode.Parent.Children.Remove(oldNode);
                _dataGrid.SavedFilesProperties.Remove(properties);
            }
        }

        private (INode, INode) AddNewNode()
        {
            INode oldNode = _treeView.SelectedNodes.First();
            NodeViewModel node = GetNewNode(oldNode);
            _treeView.SelectedNodes[1].Children.Add(node);
            return (oldNode, node);
        }

        private NodeViewModel GetNewNode(INode oldNode)
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

        private FileProperties AddNewProperties(INode oldNode, INode newNode)
        {
            FileProperties properties = _dataGrid.SavedFilesProperties.Find(x => x.Node == oldNode);
            FileProperties newProperties = GetNewProperties(properties, newNode);
            int i = _dataGrid.SavedFilesProperties.FindIndex(x => x.Node == _treeView.SelectedNodes[1]);
            _dataGrid.SavedFilesProperties.Insert(i + 1, newProperties);
            return properties;
        }

        private FileProperties GetNewProperties(FileProperties oldProperties, INode node) 
        {
            return new FileProperties(oldProperties.Name, oldProperties.Size, oldProperties.Modificated,
                oldProperties.IconPath, node);
        }
    }
}
