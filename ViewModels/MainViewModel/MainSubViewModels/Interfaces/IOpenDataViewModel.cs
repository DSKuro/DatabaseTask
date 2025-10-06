using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces
{
    public interface IOpenDataViewModel
    {
        public Task<IEnumerable<IStorageFile>?> ChooseDbFile();
        public Task OpenFolder();
    }
}
