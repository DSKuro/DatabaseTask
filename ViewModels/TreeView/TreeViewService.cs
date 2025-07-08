using Avalonia.Controls;
using DatabaseTask.Services._serviceCollection;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.EventArguments;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System;

namespace DatabaseTask.ViewModels.TreeView
{
    public class TreeViewService : ViewModelBase, ITreeView
    {
        private SmartCollection<INode> _nodes = new SmartCollection<INode>();

        public INode DraggedItem { get; set; }

        public SmartCollection<INode> SelectedNodes { get; } = new();

        public SmartCollection<INode> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }

        public EventHandler<SelectionChangedEventArgs> SelectionChanged { get; set; }
        public EventHandler<TreeViewEventArgs> ScrollChanged { get; set; }
    }
}
