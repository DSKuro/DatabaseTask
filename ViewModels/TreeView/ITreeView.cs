using DatabaseTask.ViewModels.Nodes;
using System;
using System.Collections.ObjectModel;

namespace DatabaseTask.ViewModels.TreeView
{
    public interface ITreeView
    {
        public INode SelectedNode { get; set; }

        public INode DraggedItem { get; set; }

        public ObservableCollection<INode> Nodes { get; set; }

        public event Action<INode, INode> SelectionChanged;
    }
}
