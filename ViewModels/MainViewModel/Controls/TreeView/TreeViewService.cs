using Avalonia.Controls;
using DatabaseTask.Models;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.TreeView
{
    public class TreeViewService : ViewModelBase, ITreeView
    {
        private readonly StringComparer _stringComparer;

        private SmartCollection<INode> _nodes = new SmartCollection<INode>();

        public INode? DraggedItem { get; set; }

        public SmartCollection<INode> SelectedNodes { get; } = new();

        public SmartCollection<INode> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }

        public EventHandler<SelectionChangedEventArgs> SelectionChanged { get; set; } = null!;
        public EventHandler<TreeViewEventArgs> ScrollChanged { get; set; } = null!;

        public TreeViewService()
        {
            CultureInfo culture = new CultureInfo("en-EN");
            _stringComparer = StringComparer.Create(culture, false);
        }

        public bool IsNodeExist(INode node, string name)
        {
            if (node == null)
            {
                return false;
            }

            return node.Children.Any(x => x.Name == name);
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

        public void AddSelectedNodeByIndex(int index)
        {
            if (index < 0 || index > Nodes.Count)
            {
                return;
            }

            SelectedNodes.Add(Nodes[index]);
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
    }
}
