using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality.Interfaces
{
    public interface ITreeViewFunctionality
    {
        public bool IsNodeExist(INode parent, string name);
        public bool IsParentHasNodeWithName(INode node, string name);
        public bool TryInsertNode(INode parent, INode node, out int index);
        public void AddSelectedNodeByIndex(int index);
        public int GetNodePositionIndex(INode target, INode node);
        public INode? GetFirstSelectedNode();
        public void UpdateSelectedNodes(INode node);
    }
}
