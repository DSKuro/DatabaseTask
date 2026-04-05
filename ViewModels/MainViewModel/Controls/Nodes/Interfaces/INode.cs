using Avalonia.Platform.Storage;
using DatabaseTask.Models;
using System;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces
{
    public interface INode
    {
        public bool IsExpanded { get; set; }
        public bool IsOperationHighlighted { get; set; }
        public bool IsLoaded { get; set; }
        public IStorageItem? StorageItem { get; set; }
        public string Name { get; set; }
        public INode? Parent { get; set; }
        public event Action<INode> Expanded;
        public event Action<INode> Collapsed;
        public SmartCollection<INode> Children { get; set; }
    }
}
