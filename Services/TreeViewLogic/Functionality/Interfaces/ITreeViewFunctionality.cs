using DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces
{
    public interface ITreeViewFunctionality : ITreeViewNodeService, ITreeViewSelectionService, ITreeViewSortService
    {
        public bool TryInsertNode(INode parent, INode node, out int index);
        public Task RecursiveCopyChildren(INode sourceParent, INode targetParent);
    }
}
