using DatabaseTask.Services.Collection;
using System;
using System.Collections.ObjectModel;

namespace DatabaseTask.ViewModels.Nodes
{
    public interface INode
    {
        public bool IsExpanded { get; set; }
        public INode? Parent { get; set; }
        public event Action<INode> Expanded;
        public event Action<INode> Collapsed;
        public SmartCollection<INode> Children { get; set; }
    }
}
