using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DatabaseTask.Models.StorageOptions;
using DatabaseTask.Services.Dialogues.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Dialogues.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IDialogueHelper _dialogueHelper;

        public StorageService(IDialogueHelper dialogueHelper)
        {
            _dialogueHelper = dialogueHelper;
        }

        public async Task<IEnumerable<IStorageFile>> OpenFilesAsync(object context, FilePickerOptions options)
        {
            TopLevel topLevel = _dialogueHelper.GetTopLevelForAnyDialogue(context);
            return await OpenFilesImpl(context, options, topLevel);
        }

        private async Task<IEnumerable<IStorageFile>> OpenFilesImpl(object context,
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

        public async Task<IEnumerable<IStorageFolder>> OpenFoldersAsync(object context, FolderPickerOptions options)
        {
            TopLevel topLevel = _dialogueHelper.GetTopLevelForAnyDialogue(context);
            return await OpenFoldersImpl(context, options, topLevel);
        }

        private async Task<IEnumerable<IStorageFolder>> OpenFoldersImpl(object context,
            FolderPickerOptions options,
            TopLevel topLevel)
        {
            IReadOnlyList<IStorageFolder> storageFolders = await topLevel.StorageProvider
                .OpenFolderPickerAsync(new FolderPickerOpenOptions()
                {
                    AllowMultiple = options.AllowMultiple
                });
            return storageFolders;
        }
    }
}
