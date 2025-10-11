using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Interfaces
{
    public interface IMessageBoxCommandsViewModel
    {
        public Task DeleteFolderImpl();
        public Task CopyFolderImpl();
        public Task CopyFileImpl();
        public Task MoveFileImpl();
        public Task DeleteFileImpl();
    }
}
