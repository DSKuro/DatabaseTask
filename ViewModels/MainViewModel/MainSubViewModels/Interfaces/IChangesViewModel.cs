using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces
{
    public interface IChangesViewModel
    {
        public Task ApplyChanges();
        public Task CancelChanges();
    }
}
