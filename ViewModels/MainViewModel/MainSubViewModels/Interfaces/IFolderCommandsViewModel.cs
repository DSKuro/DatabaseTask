using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces
{
    public interface IFolderCommandsViewModel
    {
        public Task CreateFolderImpl();
        public Task RenameFolderImpl();
    }
}
