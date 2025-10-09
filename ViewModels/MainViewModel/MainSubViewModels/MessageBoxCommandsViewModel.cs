using DatabaseTask.Models;
using DatabaseTask.Models.Categories;
using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class MessageBoxCommandsViewModel : BaseFolderCommandsViewModel, IMessageBoxCommandsViewModel
    {
        private readonly IFileManager _fileManager;

        public MessageBoxCommandsViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IServiceProvider serviceProvider)
            : base(messageBoxService, itemCommandsFactory, fileCommandsFactory,
                  commandsHistory, fullPath, serviceProvider) 
        { 
            _fileManager = fileManager;
        }

        public async Task DeleteFolderImpl()
        {
            await ProcessCommand(new LoggerCommandDTO(CommandType.DeleteItem,
                _fileManager.FolderPermissions.CanDeleteFolder,
                async () => await OpenMessageBox(MessageBoxCategory.DeleteFolderMessageBox.Title,
                MessageBoxCategory.DeleteFolderMessageBox.Content),
                LogCategory.DeleteFolderCategory,
                false, 
                (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name));
        }

        public async Task CopyFolderImpl()
        {
            await ProcessCommand(new LoggerCommandDTO(CommandType.CopyItem,
                _fileManager.FolderPermissions.CanCopyCatalog,
                async () => await OpenMessageBox(MessageBoxCategory.CopyFolderMessageBox.Title,
                MessageBoxCategory.CopyFolderMessageBox.Content),
                LogCategory.CopyFolderCategory,
                false,
                (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name,
                (_fileManager.TreeView.SelectedNodes[1] as NodeViewModel).Name));
        }


        public async Task MoveFileImpl()
        {
            await ProcessCommand(new LoggerCommandDTO(CommandType.MoveFile, 
                _fileManager.FilePermissions.CanCopyFile,
                async () => await OpenMessageBox(MessageBoxCategory.MoveFileMessageBox.Title,
                MessageBoxCategory.MoveFileMessageBox.Content),
                 LogCategory.MoveFileCategory,
                false,
                 (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name,
                (_fileManager.TreeView.SelectedNodes[1] as NodeViewModel).Name));
        }

        public async Task CopyFileImpl()
        {
            await ProcessCommand(new LoggerCommandDTO(CommandType.CopyItem, 
                _fileManager.FilePermissions.CanCopyFile,
                async () => await OpenMessageBox(MessageBoxCategory.CopyFileMessageBox.Title,
                MessageBoxCategory.CopyFileMessageBox.Content),
                LogCategory.CopyFileCategory,
                false,
                (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name,
                (_fileManager.TreeView.SelectedNodes[1] as NodeViewModel).Name));
        }

        public async Task DeleteFileImpl()
        {
            await ProcessCommand(new LoggerCommandDTO(CommandType.DeleteItem,
                _fileManager.FilePermissions.CanDeleteFile,
                async () => await OpenMessageBox(MessageBoxCategory.DeleteFileMessageBox.Title,
                MessageBoxCategory.DeleteFileMessageBox.Content),
                LogCategory.DeleteFileCategory,
                false,
                (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name));
        }

        private async Task<ButtonResult?> OpenMessageBox(string title, string content)
        {
            return await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                title,
                content,
                ButtonEnum.YesNo), null);
        }
    }
}
