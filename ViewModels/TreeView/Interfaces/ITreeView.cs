using Avalonia.Controls;
using DatabaseTask.Services.Collection;
using DatabaseTask.ViewModels.Nodes;
using System;

namespace DatabaseTask.ViewModels.TreeView.Interfaces
{
    public interface ITreeView
    {
        public SmartCollection<INode> SelectedNodes { get; }
        public SmartCollection<INode> Nodes { get; set; }

        public EventHandler<SelectionChangedEventArgs> SelectionChanged { get; set; }
    }
}
