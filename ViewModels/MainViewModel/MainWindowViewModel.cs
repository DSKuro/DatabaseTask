using CommunityToolkit.Mvvm.Input;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.Logger.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel
{
    public partial class MainWindowViewModel : ViewModelMessageBox
    {
        private readonly IFileManager _fileManager;
        private readonly ILogger _logger;

        private readonly IOpenDataViewModel _openDataViewModel;
        private readonly IMessageBoxCommandsViewModel _messageBoxCommandsViewModel;
        private readonly IFolderCommandsViewModel _folderCommandsViewModel;

        private readonly ICommandsHistory _commandsHistory;

        public IFileManager FileManager { get => _fileManager; }
        public ILogger Logger { get => _logger; }

        public MainWindowViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager,
            ILogger logger,
            IOpenDataViewModel openDataViewModel,
            IMessageBoxCommandsViewModel messageBoxCommandsViewModel,
            IFolderCommandsViewModel folderCommandsViewModel,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory) : base(messageBoxService)
        {
            _fileManager = fileManager;
            _logger = logger;
            _openDataViewModel = openDataViewModel;
            _messageBoxCommandsViewModel = messageBoxCommandsViewModel;
            _folderCommandsViewModel = folderCommandsViewModel;
            _commandsHistory = commandsHistory;
        }

        [RelayCommand]
        public async Task OpenDb()
        {
            await _openDataViewModel.ChooseDbFile();
        }

        [RelayCommand]
        public async Task OpenFolder()
        {
            await _openDataViewModel.OpenFolder();
        }

        [RelayCommand]
        public async Task CreateFolder()
        {
            await _folderCommandsViewModel.CreateFolderImpl();
        }

        [RelayCommand]
        public async Task RenameFolder()
        {
            await _folderCommandsViewModel.RenameFolderImpl();
        }

        [RelayCommand]
        public async Task DeleteFolder()
        {
            await _messageBoxCommandsViewModel.DeleteFolderImpl();
        }

        [RelayCommand]
        public async Task CopyFolder()
        {
            await _messageBoxCommandsViewModel.CopyFolderImpl();
        }

        [RelayCommand]
        public async Task MoveFile()
        {
            await _messageBoxCommandsViewModel.MoveFileImpl();
        }

        [RelayCommand]
        public async Task CopyFile()
        {
            await _messageBoxCommandsViewModel.CopyFileImpl();
        }

        [RelayCommand]
        public async Task DeleteFile()
        {
            await _messageBoxCommandsViewModel.DeleteFileImpl();
        }

        [RelayCommand]
        public void ApplyChanges()
        {
            _commandsHistory.ExecuteAllCommands();
        }
    }
}
