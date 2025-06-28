using System;

namespace DatabaseTask.ViewModels.Nodes
{
    public interface INode
    {
        public bool IsExpanded { get; set; }
        public event Action<INode> Expanded;
        public event Action<INode> Collapsed;
    }
}
