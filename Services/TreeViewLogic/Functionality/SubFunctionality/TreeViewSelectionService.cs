using DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality
{
    public class TreeViewSelectionService : ITreeViewSelectionService
    {
        private readonly ITreeView _treeView;

        public TreeViewSelectionService(ITreeView treeView)
        {
            _treeView = treeView;
        }

        public INode? GetFirstSelectedNode()
        {
            return _treeView.SelectedNodes[0] ?? null;
        }

        public List<INode> GetAllSelectedNodes()
        {
            return _treeView.SelectedNodes.ToList();
        }

        public void UpdateSelectedNodes(INode node)
        {
            INode? selectedNode = GetFirstSelectedNode();
            if (selectedNode != null)
            {
                selectedNode.IsExpanded = true;
            }
            _treeView.SelectedNodes.Clear();
            _treeView.SelectedNodes.Add(node);
        }

        public void RemoveNode(INode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Children.Remove(node);
            }
        }

        public void RemoveSelectedNodes(INode node)
        {
            _treeView.SelectedNodes.Remove(node);
        }

        public void AddSelectedNodeByIndex(int index)
        {
            if (index < 0 || index > _treeView.Nodes.Count)
            {
                return;
            }

            _treeView.SelectedNodes.Add(_treeView.Nodes[index]);
        }
    }
}
