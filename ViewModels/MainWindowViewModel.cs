using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Platform.Storage;
using AvaloniaEdit.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using MessageBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabaseTask.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IStorageService _storageService;
        private readonly IMessageBoxService _messageBoxService;
        public readonly IGetTreeNodes _getTreeNodes;

        private IEnumerable<IStorageFolder> _folders;

        public IGetTreeNodes GetTreeNodes { get => _getTreeNodes; }

        public MainWindowViewModel(IStorageService storageService, IMessageBoxService messageBoxService,
            IGetTreeNodes getTreeNodes)
        {
            _storageService = storageService;
            _messageBoxService = messageBoxService;
            _getTreeNodes = getTreeNodes;
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
                    new FilePickerOptions(StorageConstants.BaseStorage.Value,
                    StorageConstants.BaseStorage.Type));
            }
            catch (ArgumentNullException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Аргумент не найден",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (ArgumentException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Неверный аргумент",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (InvalidOperationException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Запрещённая операция",
                    ButtonEnum.Ok), ErrorCallback);
            }
            return null;
        }

        [RelayCommand]
        public async Task OpenFolder()
        {
            await ChooseMainFolder();
            await _getTreeNodes.GetCollectionFromFolders(_folders);
        }

        public async Task ChooseMainFolder()
        {
            try
            {
                _folders = await _storageService.OpenFoldersAsync(this,
                    new FolderPickerOptions(false));
            }
            catch (ArgumentNullException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Аргумент не найден",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (ArgumentException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Неверный аргумент",
                    ButtonEnum.Ok), ErrorCallback);
            }
            catch (InvalidOperationException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Запрещённая операция",
                    ButtonEnum.Ok), ErrorCallback);
            }
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
