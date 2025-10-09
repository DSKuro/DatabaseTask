using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Interfaces
{
    public interface IFolderCommandsViewModel
    {
        public Task CreateFolderImpl();
        public Task RenameFolderImpl();
    }
}
