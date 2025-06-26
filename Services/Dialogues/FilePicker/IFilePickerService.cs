using Avalonia.Platform.Storage;
using DatabaseTask.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Dialogues.FilePicker
{
    public interface IFilePickerService
    {
        public Task<IEnumerable<IStorageFile>> OpenFilesAsync(object context, FilePickerOptions options); 
    }
}
