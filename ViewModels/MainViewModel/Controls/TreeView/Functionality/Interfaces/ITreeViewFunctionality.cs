using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality.Interfaces
{
    public interface ITreeViewFunctionality
    {
        public bool IsNodeExist(INode parent, string name);
        public bool IsParentHasNodeWithName(INode node, string name);
        public int GetNodePositionIndex(INode target, INode node);
        public INode? GetFirstSelectedNode();
        public List<INode> GetAllSelectedNodes();
        public INode? GetChildrenByName(INode node, string name);
        public bool TryInsertNode(INode parent, INode node, out int index);
        public void AddSelectedNodeByIndex(int index);
        public INode? CreateNode(INode template, INode parent);
        public void UpdateSelectedNodes(INode node);
        public void RemoveNode(INode node);
        public void RemoveSelectedNodes(INode node);
    }
}
