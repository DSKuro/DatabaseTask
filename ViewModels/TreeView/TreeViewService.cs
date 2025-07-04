using Avalonia.Controls;
using DatabaseTask.Services.Collection;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace DatabaseTask.ViewModels.TreeView
{
    public class TreeViewService : ViewModelBase, ITreeView
    {
        private SmartCollection<INode> _nodes = new();
        private ObservableCollection<INode> _selectedNodes;

        public EventHandler<SelectionChangedEventArgs> SelectionChanged { get; set; }

        public INode DraggedItem { get; set; }

        public SmartCollection<INode> SelectedNodes { get; } = new();

        public SmartCollection<INode> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }
    }
}
