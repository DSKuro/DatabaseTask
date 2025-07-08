using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Services.Collection;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.FileManager.Interfaces;
using DatabaseTask.ViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainSubViewModels
{
    public class FileManagerFolderCommandsViewModel : ViewModelMessageBox, IFileManagerFolderCommandsViewModel
    {
        private readonly IFileManager _fileManager;
        private readonly IItemCommandsFactory _itemCommandsFactory;

        public FileManagerFolderCommandsViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager, IItemCommandsFactory itemCommandsFactory)
            : base(messageBoxService) 
        { 
            _fileManager = fileManager;
            _itemCommandsFactory = itemCommandsFactory;
        }

        public async Task CreateFolderImpl()
        {
            await ProcessCommand(_fileManager.FolderPermissions.CanDoOperationOnFolder,
                CreateNewFolder);
        }

        private async Task ProcessCommand(Action permission, Func<Task> command)
        {
            try
            {
                permission.Invoke();
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
                return;
            }
            await command.Invoke();
        }

        private async Task CreateNewFolder()
        {
            string folderName = await GetNewFolderName();
            if (folderName != null)
            {
                ICommand createFolderCommand = _itemCommandsFactory.CreateCreateFolderCommand(folderName);
                createFolderCommand.Execute();
            }
        }

        private async Task<string> GetNewFolderName()
        {
            return await WeakReferenceMessenger.Default.Send<MainWindowCreateFolderMessage>();
        }

        public async Task RenameFolderImpl()
        {
            await ProcessCommand(_fileManager.FolderPermissions.CanDoOperationOnFolder,
                RenameFolderCommandImpl);
        }

        private async Task RenameFolderCommandImpl()
        {
            string newName = await GetRenamedFolderName();
            if (newName != null)
            {
                ICommand renameFolderCommand = _itemCommandsFactory.CreateRenameFolderCommand(newName);
                renameFolderCommand.Execute();
            }
        }

        private async Task<string> GetRenamedFolderName()
        {
            return await WeakReferenceMessenger.Default.Send<MainWindowRenameFolderMessage>();
        }

        public async Task DeleteFolderImpl()
        {
            await ProcessCommand(_fileManager.FolderPermissions.CanDeleteFolder,
                DeleteFolderCommand);
        }

        private async Task DeleteFolderCommand()
        {
            await DeleteItemCommandImpl(new MessageBoxOptions(
                MessageBoxCategory.DeleteFolderMessageBox.Title,
                MessageBoxCategory.DeleteFolderMessageBox.Content,
                ButtonEnum.YesNo));
        }

        private async Task DeleteItemCommandImpl(MessageBoxOptions messageBoxOptions)
        {
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
                messageBoxOptions, null);
            if (result.HasValue && result.Value == ButtonResult.Yes)
            {
                ICommand deleteItemCommand = _itemCommandsFactory.CreateDeleteItemCommand();
                deleteItemCommand.Execute();
            }
        }

        public async Task CopyFolderImpl()
        {
            await ProcessCommand(_fileManager.FolderPermissions.CanCopyCatalog,
                CopyFolderCommand);
        }

        private async Task CopyFolderCommand()
        {
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
                new MessageBoxOptions(
                MessageBoxCategory.CopyFolderMessageBox.Title,
                MessageBoxCategory.CopyFolderMessageBox.Content,
                ButtonEnum.YesNo), null);
            if (result.HasValue && result.Value == ButtonResult.Yes)
            {
                ICommand deleteItemCommand = _itemCommandsFactory.CreateCopyFolderCommand(true);
                deleteItemCommand.Execute();
            }
        }

        public async Task MoveFileImpl()
        {
            await ProcessCommand(_fileManager.FilePermissions.CanCopyFile,
                MoveFileCommand);
        }

        private async Task MoveFileCommand()
        {
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
                new MessageBoxOptions(
                MessageBoxCategory.MoveFileMessageBox.Title,
                MessageBoxCategory.MoveFileMessageBox.Content,
                ButtonEnum.YesNo), null);
            if (result.HasValue && result.Value == ButtonResult.Yes)
            {
                ICommand deleteItemCommand = _itemCommandsFactory.CreateCopyFolderCommand(false);
                deleteItemCommand.Execute();
            }
        }

        public async Task CopyFileImpl()
        {
            await ProcessCommand(_fileManager.FilePermissions.CanCopyFile,
                CopyFileCommand);
        }

        private async Task CopyFileCommand()
        {
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
                new MessageBoxOptions(
                MessageBoxCategory.CopyFileMessageBox.Title,
                MessageBoxCategory.CopyFileMessageBox.Content,
                ButtonEnum.YesNo), null);
            if (result.HasValue && result.Value == ButtonResult.Yes)
            {
                ICommand deleteItemCommand = _itemCommandsFactory.CreateCopyFolderCommand(true);
                deleteItemCommand.Execute();
            }
        }

        public async Task DeleteFileImpl()
        {
            await ProcessCommand(_fileManager.FilePermissions.CanDeleteFile,
                DeleteFileCommand);
        }

        private async Task DeleteFileCommand()
        {
            await DeleteItemCommandImpl(new MessageBoxOptions(
                MessageBoxCategory.DeleteFileMessageBox.Title,
                MessageBoxCategory.DeleteFileMessageBox.Content,
                ButtonEnum.YesNo));
        }

 
    }
}
