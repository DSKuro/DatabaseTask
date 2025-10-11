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
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels
{
    public class FolderCommandsViewModel : BaseFolderCommandsViewModel, ICreateFolderCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;

        public FolderCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFolderOperationsPermissions folderPermissions)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath)
        {
            _folderPermissions = folderPermissions;
        }

        public async Task CreateFolder()
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
    }
}
