using DatabaseTask.Models;
using DatabaseTask.Services.Collection;
using DatabaseTask.Services.Commands.Enum;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.ViewModels.FileManager.Interfaces;
using DatabaseTask.ViewModels.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainSubViewModels
{
    public class MessageBoxCommandsViewModel : BaseFolderCommandsViewModel, IMessageBoxCommandsViewModel
    {
        private readonly IFileManager _fileManager;

        public MessageBoxCommandsViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager, ICommandsFactory itemCommandsFactory)
            : base(messageBoxService, itemCommandsFactory) 
        { 
            _fileManager = fileManager;
        }

        public async Task DeleteFolderImpl()
        {
            await ProcessCommand(_fileManager.FolderPermissions.CanDeleteFolder,
                async () => await OpenMessageBox(MessageBoxCategory.DeleteFolderMessageBox.Title,
                MessageBoxCategory.DeleteFolderMessageBox.Content),
                CommandType.DeleteItem, LogCategory.DeleteFolderCategory,
                false, (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name);
        }

        public async Task CopyFolderImpl()
        {
            await ProcessCommand(_fileManager.FolderPermissions.CanCopyCatalog,
                async () => await OpenMessageBox(MessageBoxCategory.CopyFolderMessageBox.Title,
                MessageBoxCategory.CopyFolderMessageBox.Content),
                CommandType.CopyItem, LogCategory.CopyFolderCategory,
                false,
                (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name,
                (_fileManager.TreeView.SelectedNodes[1] as NodeViewModel).Name);
        }


        public async Task MoveFileImpl()
        {
            await ProcessCommand(_fileManager.FilePermissions.CanCopyFile,
                async () => await OpenMessageBox(MessageBoxCategory.MoveFileMessageBox.Title,
                MessageBoxCategory.MoveFileMessageBox.Content),
                CommandType.MoveFile, LogCategory.MoveFileCategory,
                false,
                 (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name,
                (_fileManager.TreeView.SelectedNodes[1] as NodeViewModel).Name);
        }

        public async Task CopyFileImpl()
        {
            await ProcessCommand(_fileManager.FilePermissions.CanCopyFile,
                async () => await OpenMessageBox(MessageBoxCategory.CopyFileMessageBox.Title,
                MessageBoxCategory.CopyFileMessageBox.Content),
                CommandType.CopyItem, LogCategory.CopyFileCategory,
                false,
                (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name,
                (_fileManager.TreeView.SelectedNodes[1] as NodeViewModel).Name);
        }

        public async Task DeleteFileImpl()
        {
            await ProcessCommand(_fileManager.FilePermissions.CanDeleteFile,
                async () => await OpenMessageBox(MessageBoxCategory.DeleteFileMessageBox.Title,
                MessageBoxCategory.DeleteFileMessageBox.Content),
                CommandType.DeleteItem, LogCategory.DeleteFileCategory,
                false,
                (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name);
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
