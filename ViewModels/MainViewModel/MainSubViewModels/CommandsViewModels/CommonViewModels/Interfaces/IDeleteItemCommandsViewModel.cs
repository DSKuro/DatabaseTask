using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels.Interfaces
{
    public interface IDeleteItemCommandsViewModel
    {
        public Task DeleteItems();
        public Task DeleteFolders();
        public Task DeleteFiles();
    }
}
