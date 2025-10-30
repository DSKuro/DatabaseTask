using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces
{
    public interface ITreeViewNodeService
    {
        public bool IsNodeExist(INode parent, string name);
        public bool IsParentHasNodeWithName(INode node, string name);
        public INode? GetChildrenByName(INode node, string name);
        public INode? CreateNode(INode template, INode parent);
        public void RemoveNode(INode node);
    }
}
