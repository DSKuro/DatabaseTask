using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Collection;
using DatabaseTask.Services.Commands.Enum;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.FilesOperations.Interfaces;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class FolderCommandsViewModel : BaseFolderCommandsViewModel, IFolderCommandsViewModel
    {
        private IFileManager _fileManager;

        public FolderCommandsViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IServiceProvider serviceProvider)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath,
                  serviceProvider)
        {
            _fileManager = fileManager;
        }

        protected override bool CanExecuteCommand(object? data)
        {
            if (data == null)
            {
                return false;
            }
            return true;
        }

        public async Task CreateFolderImpl()
        {
            await ProcessCommand(new LoggerCommandDTO(CommandType.CreateFolder,
                _fileManager.FolderPermissions.CanDoOperationOnFolder,
                async () => await WeakReferenceMessenger.Default.Send<MainWindowCreateFolderMessage>(),
                LogCategory.CreateFolderCategory,
                true));
        }
        
        public async Task RenameFolderImpl()
        {
            await ProcessCommand(new LoggerCommandDTO(CommandType.RenameFolder,
                _fileManager.FolderPermissions.CanDoOperationOnFolder,
                async () => await WeakReferenceMessenger.Default.Send<MainWindowRenameFolderMessage>(),
                LogCategory.RenameFolderCategory,
                false, (_fileManager.TreeView.SelectedNodes[0] as NodeViewModel).Name));
        }
    }
}
