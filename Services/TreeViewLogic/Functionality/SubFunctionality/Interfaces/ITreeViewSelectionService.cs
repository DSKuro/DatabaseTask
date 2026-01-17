using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces
{
    public interface ITreeViewSelectionService
    {
        public INode? GetFirstSelectedNode();
        public List<INode> GetAllSelectedNodes();
        public void UpdateSelectedNodes(INode node);
        public void AddNodeToSelected(INode node);
        public void RemoveSelectedNodes(INode node);
        public void AddSelectedNodeByIndex(int index);
    }
}
