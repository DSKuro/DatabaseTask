using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.TreeViewLogic.Functionality
{
    public class TreeViewFunctionality : ITreeViewFunctionality
    {
        private readonly ITreeViewNodeService _nodeService;
        private readonly ITreeViewSelectionService _selectionService;
        private readonly ITreeViewSortService _sortService;

        public TreeViewFunctionality(ITreeViewNodeService nodeService,
            ITreeViewSelectionService selectionService,
            ITreeViewSortService sortServices)
        {
            _nodeService = nodeService;
            _selectionService = selectionService;
            _sortService = sortServices;
        }

        public bool TryInsertNode(INode parent, INode node, out int index)
        {
            index = _sortService.GetNodePositionIndex(parent, node);
            if (index == -1)
                return false;

            parent.Children.Insert(index, node);
            return true;
        }

        public bool IsNodeExist(INode parent, string name) => _nodeService.IsNodeExist(parent, name);
        public bool IsParentHasNodeWithName(INode node, string name) => _nodeService.IsParentHasNodeWithName(node, name);
        public int GetNodePositionIndex(INode parent, INode node) => _sortService.GetNodePositionIndex(parent, node);
        public INode? GetFirstSelectedNode() => _selectionService.GetFirstSelectedNode();
        public INode? GetChildrenByName(INode node, string name) => _nodeService.GetChildrenByName(node, name);
        public List<INode> GetAllSelectedNodes() => _selectionService.GetAllSelectedNodes();
        public INode? GetCoreNode() => _nodeService.GetCoreNode();
        public INode? CreateNode(INode template, INode parent) => _nodeService.CreateNode(template, parent);
        public void RemoveNode(INode node) => _nodeService.RemoveNode(node);
        public void UpdateSelectedNodes(INode node) => _selectionService.UpdateSelectedNodes(node);
        public void RemoveSelectedNodes(INode node) => _selectionService.RemoveSelectedNodes(node);
        public void AddSelectedNodeByIndex(int index) => _selectionService.AddSelectedNodeByIndex(index);
    }
}
