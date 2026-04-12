using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces
{
    public interface ITreeViewNodeService
    {
        public bool IsNodeExist(INode parent, string name);
        public bool IsParentHasNodeWithName(INode node, string name);
        public INode? GetChildrenByName(INode node, string name);
        public INode? GetNodeByPath(string path);
        public INode? FindVirtualNode(string name);
        public INode? GetCoreNode();
        public INode? CreateNode(INode template, INode parent);
        public Task<List<INode>> CreateNodesFromPathsAsync(IEnumerable<string> paths, INode? parent = null);
        public Task<List<INode>> GetChildNodesAsync(INode node);
        public void UpdatePathRecursive(INode node, string path);
        public void RemoveNode(INode node);
        public void BringIntoView(INode node);
    }
}
