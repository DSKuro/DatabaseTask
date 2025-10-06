using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Models.StorageOptions;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using DatabaseTask.Services.FilesOperations.Interfaces;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class OpenDataViewModel : ViewModelMessageBox, IOpenDataViewModel
    {
        private readonly IStorageService _storageService;
        private readonly IFileManager _fileManager;
        private readonly IFullPath _fullPath;

        public OpenDataViewModel(IMessageBoxService messageBoxService,
            IStorageService storageService,
            IFileManager fileManager,
            IFullPath fullPath)
            : base(messageBoxService)
        {
            _storageService = storageService;
            _fileManager = fileManager;
            _fullPath = fullPath;
        }

        public async Task<IEnumerable<IStorageFile>?> ChooseDbFile()
        {
            try
            {
                return await _storageService.OpenFilesAsync("MainDialogueWindow",
                    new FilePickerOptions(StorageConstants.BaseStorage.Value,
                    StorageConstants.BaseStorage.Type));
            }
            catch (ArgumentNullException)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Аргумент не найден",
                    ButtonEnum.Ok));
            }
            catch (ArgumentException)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Неверный аргумент",
                    ButtonEnum.Ok));
            }
            catch (InvalidOperationException)
            {
                await MessageBoxHelper("MainDialogueWindow",new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Запрещённая операция",
                    ButtonEnum.Ok));
            }
            return null;
        }

        public async Task OpenFolder()
        {
            IEnumerable<IStorageFolder>? folders = await ChooseMainFolder();
            if (folders == null)
            {
                return;
            }

            foreach (IStorageFolder folder in folders)
            {
                _fullPath.PathToCoreFolder = folder.Path.AbsolutePath;   
            }
            await _fileManager.GetCollectionFromFolders(folders);
        }

        private async Task<IEnumerable<IStorageFolder>?> ChooseMainFolder()
        {
            try
            {
                return await ChooseMainFolderImpl();
            }
            catch (ArgumentNullException)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Аргумент не найден",
                    ButtonEnum.Ok));
            }
            catch (ArgumentException)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Неверный аргумент",
                    ButtonEnum.Ok));
            }
            catch (InvalidOperationException)
            {
                await MessageBoxHelper("MainDialogueWindow",new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Запрещённая операция",
                    ButtonEnum.Ok));
            }
            return null;
        }

        private async Task<IEnumerable<IStorageFolder>> ChooseMainFolderImpl()
        {
            IEnumerable<IStorageFolder> folders = await _storageService.OpenFoldersAsync("MainDialogueWindow",
                    new FolderPickerOptions(false));
            if (folders.Count() != 0)
            {
                WeakReferenceMessenger.Default.Send<MainWindowEnableManagerButtons>();
            }
            return folders;
        }
    }
}
