using Avalonia.Platform.Storage;
using DatabaseTask.Models.StorageOptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Dialogues.Storage
{
    public interface IStorageService
    {
        public Task<IEnumerable<IStorageFile>> OpenFilesAsync(object context, FilePickerOptions options);
        public Task<IEnumerable<IStorageFolder>> OpenFoldersAsync(object context, FolderPickerOptions options);
    }
}
