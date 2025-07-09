using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Services.Commands.Enum;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.FileManager.Interfaces;
using DatabaseTask.ViewModels.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainSubViewModels
{
    public class FolderCommandsViewModel : BaseFolderCommandsViewModel, IFolderCommandsViewModel
    {
        private IFileManager _fileManager;

        public FolderCommandsViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager,
            IItemCommandsFactory itemCommandsFactory)
            : base(messageBoxService, itemCommandsFactory)
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
            await ProcessCommand(_fileManager.FolderPermissions.CanDoOperationOnFolder,
                async() => await WeakReferenceMessenger.Default.Send<MainWindowCreateFolderMessage>(),
            CommandType.CreateFolder);
        }
        
        public async Task RenameFolderImpl()
        {
            await ProcessCommand(_fileManager.FolderPermissions.CanDoOperationOnFolder,
               async () => await WeakReferenceMessenger.Default.Send<MainWindowRenameFolderMessage>(),
           CommandType.RenameFolder);
        }
    }
}
