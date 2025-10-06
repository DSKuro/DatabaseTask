using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces
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
