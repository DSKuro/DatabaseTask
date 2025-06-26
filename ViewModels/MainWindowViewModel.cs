using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using MessageBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IStorageService _storageService;
        private readonly IMessageBoxService _messageBoxService;

        public MainWindowViewModel(IStorageService storageService, IMessageBoxService messageBoxService)
        {
            _storageService = storageService;
            _messageBoxService = messageBoxService;
        }

        [RelayCommand]
        public async Task OpenDb()
        {
            await ChooseDbFile();
        }

        public async Task<IEnumerable<IStorageFile>> ChooseDbFile()
        {
            try
            {
                return await _storageService.OpenFilesAsync(this,
                    new FilePickerOptions(StorageConstants.DbChose,
                    StorageConstants.DatabaseFilter));
            }
            catch (ArgumentNullException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.ErrorTitle, "Аргумент не найден",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (ArgumentException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.ErrorTitle, "Неверный аргумент",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (InvalidOperationException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.ErrorTitle, "Запрещённая операция",
                    ButtonEnum.Ok), ErrorCallback);
            }
            return null;
        }

        [RelayCommand]
        public async Task OpenFolder()
        {
            await ChooseMainFolder();
        }

        public async Task<IEnumerable<IStorageFolder>> ChooseMainFolder()
        {
            try
            {
                return await _storageService.OpenFoldersAsync(this,
                    new FolderPickerOptions(false));
            }
            catch (ArgumentNullException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.ErrorTitle, "Аргумент не найден",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (ArgumentException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.ErrorTitle, "Неверный аргумент",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (InvalidOperationException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.ErrorTitle, "Запрещённая операция",
                    ButtonEnum.Ok), ErrorCallback);
            }
            return null;
        }

        private async Task MessageBoxHelper(MessageBoxOptions options, Action callback)
        {
            try
            {
                await _messageBoxService.ShowMessageBoxAsync(this, options);
            }
            finally
            {
                if (callback != null)
                {
                    callback.Invoke();
                }
            }
        }

        private void ErrorCallback()
        {
            Environment.Exit(1);
        }
    }
}
