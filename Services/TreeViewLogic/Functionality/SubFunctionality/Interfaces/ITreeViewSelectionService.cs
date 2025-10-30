using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces
{
    public interface ITreeViewSelectionService
    {
        INode? GetFirstSelectedNode();
        List<INode> GetAllSelectedNodes();
        void UpdateSelectedNodes(INode node);
        void RemoveSelectedNodes(INode node);
        void AddSelectedNodeByIndex(int index);
    }
}
