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
        private Smart_serviceCollection<INode> _nodes = new Smart_serviceCollection<INode>();

        public INode DraggedItem { get; set; }

        public Smart_serviceCollection<INode> SelectedNodes { get; } = new();

        public Smart_serviceCollection<INode> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }

        public EventHandler<SelectionChangedEventArgs> SelectionChanged { get; set; }
        public EventHandler<TreeViewEventArgs> ScrollChanged { get; set; }
    }
}
