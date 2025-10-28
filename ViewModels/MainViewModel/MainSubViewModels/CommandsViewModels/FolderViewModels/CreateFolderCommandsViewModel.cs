using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels
{
    public class CreateFolderCommandsViewModel : BaseOperationsCommandsViewModel, ICreateFolderCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly ITreeView _treeView;

        public CreateFolderCommandsViewModel(IMessageBoxService messageBoxService,
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

        public async Task CreateFolder()
        {
            try
            {
                await CreateFolderImplementation();
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
        }

        private async Task CreateFolderImplementation()
        {
            _folderPermissions.CanDoOperationOnFolder();
            object? data = await WeakReferenceMessenger.Default.Send<MainWindowCreateFolderMessage>();
            string? name = data?.ToString();
            if (name == null)
            {
                return;
            }

            await ProcessCreateFolder(name);
        }

        private async Task ProcessCreateFolder(string name)
        {
            if (!_treeView.IsNodeExist(_treeView.SelectedNodes[0], name))
            {
                await CreateFolderOperation(name);
                return;
            }

            await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                MessageBoxConstants.Error.Value,
                "Каталог с данным именем уже существует",
                ButtonEnum.Ok));
        }
    }
}
