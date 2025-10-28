using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels.Interfaces
{
    public interface IMoveFileCommandsViewModel
    {
        public Task CopyFile();
        public Task MoveFile();
    }
}
