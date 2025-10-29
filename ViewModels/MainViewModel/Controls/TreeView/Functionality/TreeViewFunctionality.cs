using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality
{
    public class TreeViewFunctionality : ITreeViewFunctionality
    {
        private readonly ITreeView _treeView;

        private readonly StringComparer _stringComparer;

        public TreeViewFunctionality(ITreeView treeView)
        {
            _treeView = treeView;
            CultureInfo culture = new CultureInfo("en-EN");
            _stringComparer = StringComparer.Create(culture, false);
        }

        public bool IsNodeExist(INode parent, string name)
        {
            if (parent == null)
            {
                return false;
            }

            return parent.Children.Any(x => x.Name == name);
        }

        public bool IsParentHasNodeWithName(INode node, string name)
        {
            if (node != null && node.Parent != null)
            {
                return node.Parent.Children
                    .Any(x => x.Name == name);
            }

            return false;
        }

        public bool TryInsertNode(INode parent, INode node, out int index)
        {
            index = GetNodePositionIndex(parent, node);
            if (index == -1)
            {
                return false;
            }
            parent.Children.Insert(index, node);
            return true;
        }

        public void AddSelectedNodeByIndex(int index)
        {
            if (index < 0 || index > _treeView.Nodes.Count)
            {
                return;
            }

            _treeView.SelectedNodes.Add(_treeView.Nodes[index]);
        }

        public int GetNodePositionIndex(INode target, INode node)
        {
            NodeViewModel? nodeModel = node as NodeViewModel;
            if (nodeModel == null)
            {
                return -1;
            }

            int relativeIndex = GetRelativeIndex(target, nodeModel);
            if (relativeIndex == -1 || nodeModel.IsFolder)
            {
                return relativeIndex;
            }

            int lastFolderIndex = GetLastFolderIndex(target);

            if (lastFolderIndex == -1)
            {
                return relativeIndex;
            }

            return relativeIndex + lastFolderIndex;
        }

        private int GetRelativeIndex(INode target, NodeViewModel node)
        {
            List<string> folders = target.Children
                .Select(x => x as NodeViewModel)
                .Where(x => x != null && x.IsFolder == node.IsFolder)
                .Select(x => x!.Name)
                .ToList();
            folders.Add(node.Name);
            folders.Sort(_stringComparer);
            return folders.IndexOf(node.Name);
        }

        private int GetLastFolderIndex(INode target)
        {
            return target.Children
                .Select((x, index) => new { Node = x as NodeViewModel, Index = index })
                .Where(x => x.Node != null && x.Node.IsFolder)
                .LastOrDefault()?.Index ?? -1;
        }

        public INode? GetFirstSelectedNode()
        {
            return _treeView.SelectedNodes[0] ?? null;
        }

        public List<INode> GetAllSelectedNodes()
        {
            return _treeView.SelectedNodes.ToList();
        }

        public void UpdateSelectedNodes(INode node)
        {
            INode? selectedNode = GetFirstSelectedNode();
            if (selectedNode != null)
            {
                selectedNode.IsExpanded = true;
            }
            _treeView.SelectedNodes.Clear();
            _treeView.SelectedNodes.Add(node);
        }

        public INode? GetChildrenByName(INode node, string name)
        {
            return node.Children.FirstOrDefault(x => x.Name == name);
        }

        public void RemoveNode(INode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Children.Remove(node);
            }
        }

        public void RemoveSelectedNodes(INode node)
        {
            _treeView.SelectedNodes.Remove(node);
        }
    }
}
