using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Services.Commands;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using DatabaseTask.Services.FileManagerOperations.Exceptions;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.FileManager.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public partial class MainWindowViewModel : ViewModelMessageBox
    {
        private readonly IStorageService _storageService;
        private readonly IFileManager _fileManager;
        private readonly IFolderCommandsFactory _folderCommandsFactory;

        private IEnumerable<IStorageFolder> _folders;

        public ObservableCollection<INode> Nodes { get; set;  }

        public IFileManager FileManager { get => _fileManager; }

        public MainWindowViewModel(IStorageService storageService, IMessageBoxService messageBoxService,
            IFileManager fileManager,
            IFolderCommandsFactory folderCommandsFactory) : base(messageBoxService)
        {
            _storageService = storageService;
            _fileManager = fileManager;
            _folderCommandsFactory = folderCommandsFactory;
        }

        [RelayCommand]
        public async Task CreateFolder()
        {
            await CreateFolderImpl();
        }

        private async Task CreateFolderImpl()
        {
            try
            {
                _fileManager.Permissions.CanCreateFolder();
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper(new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
                return;
            }
            await CreateNewFolder();
        }

        private async Task CreateNewFolder()
        {
            string folderName = await GetNewFolderName();
            if (folderName != null)
            {
                ICommand createFolderCommand = _folderCommandsFactory.CreateCreateFolderCommand(folderName);
                createFolderCommand.Execute();
            }
        }

        private async Task<string> GetNewFolderName()
        {
            return await WeakReferenceMessenger.Default.Send<MainWindowCreateFolderWindow>();
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
            await _fileManager.GetCollectionFromFolders(_folders);
        }

        public async Task ChooseMainFolder()
        {
            try
            {
                _folders = await _storageService.OpenFoldersAsync(this,
                    new FolderPickerOptions(false));
                WeakReferenceMessenger.Default.Send<MainWindowEnableManagerButtons>();
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
    }
}
