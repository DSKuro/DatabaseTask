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
    public class CopyItemOperation : ICopyItemOperation
    {
        private readonly IDataGrid _dataGrid;
        private readonly ITreeView _treeView;

        public CopyItemOperation(
            IDataGrid dataGrid,
            ITreeView treeView)
        {
            _dataGrid = dataGrid;
            _treeView = treeView;
        }

        public void CopyItem(INode copied, INode target, string newItemName)
        {
            INode? newNode = AddNewNode(copied, target);
            if (copied != null && newNode != null)
            {
                newNode.Name = newItemName;
                AddNewProperties(copied, newNode, target);

                RecursiveCopyChildren(copied, newNode);
            }
        }

        private void RecursiveCopyChildren(INode sourceParent, INode targetParent)
        {
            foreach (INode child in sourceParent.Children)
            {
                INode? newChildNode = AddNewNode(child, targetParent);
                if (newChildNode != null)
                {
                    AddNewProperties(child, newChildNode, targetParent);

                    if (child.Children.Any())
                    {
                        RecursiveCopyChildren(child, newChildNode);
                    }
                }
            }
        }

        private INode? AddNewNode(INode copied, INode target)
        {
            INode? node = null;
            if (copied != null)
            {
                node = GetNewNode(copied, target);
                if (node != null)
                {
                    int index = _treeView.GetNodePositionIndex(target, node);
                    if (index != -1)
                    {
                        target.Children.Insert(index, node);
                    }
                }
            }
            return node;
        }

        private NodeViewModel? GetNewNode(INode oldNode, INode target)
        {
            if (oldNode is NodeViewModel node)
            {
                SmartCollection<INode> children = new SmartCollection<INode>();
                return new NodeViewModel()
                {
                    Name = node.Name,
                    IsExpanded = node.IsExpanded,
                    IsFolder = node.IsFolder,
                    IconPath = node.IconPath,
                    Parent = target,
                    Children = children,
                };
            }
            return null;
        }

        private void AddNewProperties(INode? oldNode, INode? newNode, INode target)
        {
            FileProperties? properties = _dataGrid.SavedFilesProperties.Find(x => x.Node == oldNode);
            if (properties != null && newNode != null)
            {
                FileProperties newProperties = GetNewProperties(properties, newNode);
                int i = _dataGrid.SavedFilesProperties.FindIndex(x => x.Node == target);
                _dataGrid.SavedFilesProperties.Insert(i + 1, newProperties);
            }
        }

        private FileProperties GetNewProperties(FileProperties oldProperties, INode node)
        {
            return new FileProperties(node.Name, oldProperties.Size, oldProperties.Modificated,
                oldProperties.IconPath, node);
        }
    }
}
