using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels.Interfaces
{
    public interface IMoveFileCommandsViewModel
    {
        public Task CopyFile();
        public Task MoveFile();
        public Task ExecuteOperation(List<INode> nodes, bool isMove, bool allowFolders);
    }
}
