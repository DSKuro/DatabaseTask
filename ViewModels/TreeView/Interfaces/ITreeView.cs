using DatabaseTask.Services.Collection;
using DatabaseTask.ViewModels.Nodes;
using System;

namespace DatabaseTask.ViewModels.TreeView.Interfaces
{
    public interface ITreeView
    {
        public INode SelectedNode { get; set; }
        public SmartCollection<INode> SelectedNodes { get; }
        public SmartCollection<INode> Nodes { get; set; }

        public event Action<INode, INode> SelectionChanged;
    }
}
