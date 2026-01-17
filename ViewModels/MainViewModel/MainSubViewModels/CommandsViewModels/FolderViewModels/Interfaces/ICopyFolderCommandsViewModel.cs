using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels.Interfaces
{
    public interface ICopyFolderCommandsViewModel
    {
        public Task CopyFolder();
        public Task CopyFolderImplementation(List<INode> nodes, bool isUpdateSelection);
    }
}
