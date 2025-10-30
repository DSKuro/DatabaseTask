using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces
{
    public interface ITreeViewSortService
    {
        public int GetNodePositionIndex(INode target, INode node);
    }
}
