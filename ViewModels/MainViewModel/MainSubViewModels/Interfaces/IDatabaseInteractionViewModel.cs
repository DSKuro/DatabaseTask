using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces
{
    public interface IDatabaseInteractionViewModel
    {
        public Task FindDuplicates();
        public Task FindUnusedFiles();
    }
}
