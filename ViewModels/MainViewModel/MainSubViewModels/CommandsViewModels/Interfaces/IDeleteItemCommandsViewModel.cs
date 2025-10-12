using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Interfaces
{
    public interface IDeleteItemCommandsViewModel
    {
        public Task DeleteFolders();
        public Task DeleteFiles();
    }
}
