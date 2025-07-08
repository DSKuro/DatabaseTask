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
        private readonly IFolderCommandsFactory _folderCommandsFactory;

        public FileManagerFolderCommandsViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager, IFolderCommandsFactory folderCommandsFactory)
            : base(messageBoxService) 
        { 
            _fileManager = fileManager;
            _folderCommandsFactory = folderCommandsFactory;
        }

        public async Task CreateFolderImpl()
        {
            await ProcessCommand(_fileManager.Permissions.CanDoOperationOnFolder,
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
                ICommand createFolderCommand = _folderCommandsFactory.CreateCreateFolderCommand(folderName);
                createFolderCommand.Execute();
            }
        }

        private async Task<string> GetNewFolderName()
        {
            return await WeakReferenceMessenger.Default.Send<MainWindowCreateFolderMessage>();
        }

        public async Task RenameFolderImpl()
        {
            await ProcessCommand(_fileManager.Permissions.CanDoOperationOnFolder,
                RenameFolderCommandImpl);
        }

        private async Task RenameFolderCommandImpl()
        {
            string newName = await GetRenamedFolderName();
            if (newName != null)
            {
                ICommand renameFolderCommand = _folderCommandsFactory.CreateRenameFolderCommand(newName);
                renameFolderCommand.Execute();
            }
        }

        private async Task<string> GetRenamedFolderName()
        {
            return await WeakReferenceMessenger.Default.Send<MainWindowRenameFolderMessage>();
        }

        public async Task DeleteFolderImpl()
        {
            await ProcessCommand(_fileManager.Permissions.CanDeleteFolder,
                DeleteCommand);
        }

        private async Task DeleteCommand()
        {
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
                new MessageBoxOptions(MessageBoxCategory.DeleteFolderMessageBox.Title,
                MessageBoxCategory.DeleteFolderMessageBox.Content, ButtonEnum.YesNo), null);
            if (result.HasValue && result.Value == ButtonResult.Yes)
            {
                ICommand deleteFolderCommand = _folderCommandsFactory.CreateDeleteFolderCommand();
                deleteFolderCommand.Execute();
            }
        }
    }
}
