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
        private readonly ICreateFolderCommandsViewModel _folderCommandsViewModel;
        private readonly IRenameFolderCommandsViewModel _renameCommandsViewModel;
        private readonly IDeleteItemCommandsViewModel _deleteItemCommandsViewModel;
        private readonly IMoveFileCommandsViewModel _moveFileCommandsViewModel;
        private readonly ICopyFolderCommandsViewModel _copyFolderCommandsViewModel;

        private readonly ICommandsHistory _commandsHistory;

        public IFileManager FileManager { get => _fileManager; }
        public ILogger Logger { get => _logger; }

        public MainWindowViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager,
            ILogger logger,
            IOpenDataViewModel openDataViewModel,
            ICreateFolderCommandsViewModel folderCommandsViewModel,
            IRenameFolderCommandsViewModel renameFolderCommandsViewModel,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IDeleteItemCommandsViewModel deleteItemCommandsViewModel,
            IMoveFileCommandsViewModel moveFileCommandsViewModel,
            ICopyFolderCommandsViewModel copyFolderCommandsViewModel) : base(messageBoxService)
        {
            _fileManager = fileManager;
            _logger = logger;
            _openDataViewModel = openDataViewModel;
            _folderCommandsViewModel = folderCommandsViewModel;
            _renameCommandsViewModel = renameFolderCommandsViewModel;
            _commandsHistory = commandsHistory;
            _deleteItemCommandsViewModel = deleteItemCommandsViewModel;
            _moveFileCommandsViewModel = moveFileCommandsViewModel;
            _copyFolderCommandsViewModel = copyFolderCommandsViewModel;
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
            await _folderCommandsViewModel.CreateFolder();
        }

        [RelayCommand]
        public async Task RenameFolder()
        {
            await _renameCommandsViewModel.RenameFolder();
        }

        [RelayCommand]
        public async Task DeleteFolder()
        {
            await _deleteItemCommandsViewModel.DeleteFolders();
        }

        [RelayCommand]
        public async Task CopyFolder()
        {
            await _copyFolderCommandsViewModel.CopyFolder();
        }

        [RelayCommand]
        public async Task MoveFile()
        {
            await _moveFileCommandsViewModel.MoveFile();
        }

        [RelayCommand]
        public async Task CopyFile()
        {
            await _moveFileCommandsViewModel.CopyFile();
        }

        [RelayCommand]
        public async Task DeleteFile()
        {
            await _deleteItemCommandsViewModel.DeleteFiles();
        }

        [RelayCommand]
        public void ApplyChanges()
        {
            _commandsHistory.ExecuteAllCommands();
        }
    }
}
