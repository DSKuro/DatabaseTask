using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces
{
    public interface IValidateViewModel
    {
        public Task<bool> ValidateCatalogAndDatabaseAsync();
    }
}
