using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.FileManager.Interfaces;
using DatabaseTask.ViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainSubViewModels
{
    public class OpenDataViewModel : ViewModelMessageBox, IOpenDataViewModel
    {
        private readonly IStorageService _storageService;
        private readonly IFileManager _fileManager;

        public OpenDataViewModel(IMessageBoxService messageBoxService,
            IStorageService storageService,
            IFileManager fileManager)
            : base(messageBoxService)
        {
            _storageService = storageService;
            _fileManager = fileManager;
        }

        public async Task<IEnumerable<IStorageFile>> ChooseDbFile()
        {
            try
            {
                return await _storageService.OpenFilesAsync("MainDialogueWindow",
                    new FilePickerOptions(StorageConstants.BaseStorage.Value,
                    StorageConstants.BaseStorage.Type));
            }
            catch (ArgumentNullException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Аргумент не найден",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (ArgumentException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Неверный аргумент",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (InvalidOperationException ex)
            {
                await MessageBoxHelper("MainDialogueWindow",new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Запрещённая операция",
                    ButtonEnum.Ok), ErrorCallback);
            }
            return null;
        }

        public async Task OpenFolder()
        {
            IEnumerable<IStorageFolder> folders = await ChooseMainFolder();
            await _fileManager.GetCollectionFromFolders(folders);
        }

        private async Task<IEnumerable<IStorageFolder>> ChooseMainFolder()
        {
            try
            {
                return await ChooseMainFolderImpl();
            }
            catch (ArgumentNullException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Аргумент не найден",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (ArgumentException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Неверный аргумент",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (InvalidOperationException ex)
            {
                await MessageBoxHelper("MainDialogueWindow",new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Запрещённая операция",
                    ButtonEnum.Ok), ErrorCallback);
            }
            return null;
        }

        private async Task<IEnumerable<IStorageFolder>> ChooseMainFolderImpl()
        {
            IEnumerable<IStorageFolder> folders = await _storageService.OpenFoldersAsync("MainDialogueWindow",
                    new FolderPickerOptions(false));
            WeakReferenceMessenger.Default.Send<MainWindowEnableManagerButtons>();
            return folders;
        }
    }
}
