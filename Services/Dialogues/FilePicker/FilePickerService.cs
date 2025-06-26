using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Dialogues.FilePicker
{
    public class FilePickerService : IFilePickerService
    {
        private readonly DialogueHelper _dialogueHelper;

        public FilePickerService(DialogueHelper dialogueHelper)
        {
            _dialogueHelper = dialogueHelper;
        }

        public async Task<IEnumerable<IStorageFile>> OpenFilesAsync(object context, FilePickerOptions options)
        {
            TopLevel topLevel = _dialogueHelper.GetTopLevelForAnyDialogue(context);
            return await OpenDialogue(context, options, topLevel);
        }

        private async Task<IEnumerable<IStorageFile>> OpenDialogue(object context, 
            FilePickerOptions options,
            TopLevel topLevel)
        {
            IReadOnlyList<IStorageFile> storageFiles = await topLevel.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions()
                {
                    AllowMultiple = options.AllowMultiple,
                    Title = options.Title ?? "Выберите файл(ы)",
                    FileTypeFilter = new[] { options.Filter }
                }
            );
            return storageFiles;
        }
    }
}
