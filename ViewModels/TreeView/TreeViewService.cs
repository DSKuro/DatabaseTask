using DatabaseTask.Services.Collection;
using DatabaseTask.ViewModels.Nodes;
using System;
using System.Collections.ObjectModel;

namespace DatabaseTask.ViewModels.TreeView
{
    public class TreeViewService : ViewModelBase, ITreeView
    {
        private INode _selectedNode;
        private ObservableCollection<INode> _nodes = new();
        private ObservableCollection<INode> _selectedNodes;

        public INode DraggedItem { get; set; }

        public INode SelectedNode
        {
            get => _selectedNode;
            set
            {
                if (_selectedNode != value)
                {
                    OnSelectionChanged(_selectedNode, value);
                    _selectedNode = value;
                    OnPropertyChanged();
                }
            }
        }

        public SmartCollection<INode> SelectedNodes { get; } = new();

        public ObservableCollection<INode> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }
 

        public event Action<INode, INode> SelectionChanged;

        private void OnSelectionChanged(INode oldNode, INode newNode)
        {
            SelectionChanged?.Invoke(oldNode, newNode);
        }
    }
}
