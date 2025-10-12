using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels
{
    public class MessageBoxCommandsViewModel : BaseFolderCommandsViewModel, IMessageBoxCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly ITreeView _treeView;

        public MessageBoxCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFolderOperationsPermissions folderPermissions,
            ITreeView treeView)
            : base(messageBoxService, itemCommandsFactory, fileCommandsFactory,
                  commandsHistory, fullPath) 
        { 
            _folderPermissions = folderPermissions;
            _treeView = treeView;
        }
        public async Task CopyFolderImpl()
        {
            try
            {
                _folderPermissions.CanCopyCatalog();
                ButtonResult? result = await OpenMessageBox(MessageBoxCategory.CopyFolderMessageBox.Title,
                    MessageBoxCategory.CopyFolderMessageBox.Content);
                if (result != null && result == ButtonResult.Yes)
                {
                    ProcessCommand(new Models.DTO.CommandInfo
                        (
                            CommandType.CopyItem
                        ),
                        new Models.DTO.LoggerDTO
                        (
                            LogCategory.CopyFolderCategory,
                            (_treeView.SelectedNodes[0] as NodeViewModel)!.Name,
                            (_treeView.SelectedNodes[1] as NodeViewModel)!.Name
                        )
                    );
                }
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
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
