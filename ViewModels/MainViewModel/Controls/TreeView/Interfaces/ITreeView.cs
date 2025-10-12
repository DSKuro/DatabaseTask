using Avalonia.Controls;
using DatabaseTask.Models;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using System;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces
{
    public interface ITreeView
    {
        public SmartCollection<INode> SelectedNodes { get; }
        public SmartCollection<INode> Nodes { get; set; }
        public EventHandler<SelectionChangedEventArgs> SelectionChanged { get; set; }
        public EventHandler<TreeViewEventArgs> ScrollChanged { get; set; }

        public bool IsNodeExist(int selectedNodeIndex, string name);
        public bool IsNodeExist(INode node, string name);
        public bool IsParentHasNodeWithName(INode node, string name);
        public void AddSelectedNodeByIndex(int index);
    }
}
