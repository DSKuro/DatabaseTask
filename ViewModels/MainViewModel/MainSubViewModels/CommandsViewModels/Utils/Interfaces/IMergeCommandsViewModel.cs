using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces
{
    public interface IMergeCommandsViewModel
    {
        public Task ProcessNodeRecursive(INode sourceChild, INode targetParent);
    }
}
