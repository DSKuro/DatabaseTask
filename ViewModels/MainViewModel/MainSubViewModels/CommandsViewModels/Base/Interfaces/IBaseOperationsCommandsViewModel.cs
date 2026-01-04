using DatabaseTask.Models.Categories;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base.Interfaces
{
    public interface IBaseOperationsCommandsViewModel
    {
        public Task CreateFolderOperation(INode node, string name);
        public Task DeleteItemOperation(INode node, LogCategory category);
        public Task CopyItemOperation(INode node, INode target, string name);
        public Task MoveItemOperation(INode node, INode target, string name);
        public Task RenameFolderOperation(INode node, string newName);
    }
}
