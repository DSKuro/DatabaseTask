using Avalonia.Controls;
using DatabaseTask.Services._serviceCollection;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.EventArguments;
using System;

namespace DatabaseTask.ViewModels.TreeView.Interfaces
{
    public interface ITreeView
    {
        public SmartCollection<INode> SelectedNodes { get; }
        public SmartCollection<INode> Nodes { get; set; }
        public EventHandler<SelectionChangedEventArgs> SelectionChanged { get; set; }
        public EventHandler<TreeViewEventArgs> ScrollChanged { get; set; }
    }
}
