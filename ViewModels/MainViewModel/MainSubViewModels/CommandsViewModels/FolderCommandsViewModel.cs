using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
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
    public class FolderCommandsViewModel : BaseFolderCommandsViewModel, IFolderCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly ITreeView _treeView;

        public FolderCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFolderOperationsPermissions folderPermissions,
            ITreeView treeView)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath)
        {
            _folderPermissions = folderPermissions;
            _treeView = treeView;
        }

        public async Task CreateFolderImpl()
        {
            try
            {
                _folderPermissions.CanDoOperationOnFolder();
                object? data = await WeakReferenceMessenger.Default.Send<MainWindowCreateFolderMessage>();
                if (data != null)
                {
                    await ProcessCommand(new Models.DTO.CommandInfo
                        (
                            CommandType.CreateFolder, data
                        ),
                        new Models.DTO.LoggerDTO
                        (
                            LogCategory.CreateFolderCategory,
                            data.ToString()!
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
        
        public async Task RenameFolderImpl()
        {
            try
            {
                _folderPermissions.CanDoOperationOnFolder();
                object? data = await WeakReferenceMessenger.Default.Send<MainWindowRenameFolderMessage>();
                if (data != null)
                {
                    await ProcessCommand(new Models.DTO.CommandInfo
                        (
                            CommandType.RenameFolder, data
                        ),
                        new Models.DTO.LoggerDTO
                        (
                            LogCategory.RenameFolderCategory,
                            (_treeView.SelectedNodes[0] as NodeViewModel)!.Name,
                            data.ToString()!
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
    }
}
