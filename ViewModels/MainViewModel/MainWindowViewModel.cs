using CommunityToolkit.Mvvm.Input;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.Logger.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels.Interfaces;
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
        private readonly IDatabaseInteractionViewModel _databaseInteractionViewModel;
        private readonly ICopyAllCommandsViewModel _copyAllCommandsViewModel;

        private readonly IChangesViewModel _changesViewModel;

        public IFileManager FileManager { get => _fileManager; }
        public ILogger Logger { get => _logger; }

        public MainWindowViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager,
            ILogger logger,
            IOpenDataViewModel openDataViewModel,
            ICreateFolderCommandsViewModel folderCommandsViewModel,
            IRenameFolderCommandsViewModel renameFolderCommandsViewModel,
            IFileCommandsFactory fileCommandsFactory,
            IDeleteItemCommandsViewModel deleteItemCommandsViewModel,
            IMoveFileCommandsViewModel moveFileCommandsViewModel,
            ICopyFolderCommandsViewModel copyFolderCommandsViewModel,
            IDatabaseInteractionViewModel databaseInteractionViewModel,
            ICopyAllCommandsViewModel copyAllCommandsViewModel,
            IChangesViewModel changesViewModel) : base(messageBoxService)
        {
            _fileManager = fileManager;
            _logger = logger;
            _openDataViewModel = openDataViewModel;
            _folderCommandsViewModel = folderCommandsViewModel;
            _renameCommandsViewModel = renameFolderCommandsViewModel;
            _deleteItemCommandsViewModel = deleteItemCommandsViewModel;
            _moveFileCommandsViewModel = moveFileCommandsViewModel;
            _copyFolderCommandsViewModel = copyFolderCommandsViewModel;
            _databaseInteractionViewModel = databaseInteractionViewModel;
            _copyAllCommandsViewModel = copyAllCommandsViewModel;
            _changesViewModel = changesViewModel;
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
        public async Task ContextMenuCopyCommand()
        {
            await _copyAllCommandsViewModel.CopyAllItems();
        }

        [RelayCommand]
        public async Task ContextMenuDeleteCommand()
        {
            await _deleteItemCommandsViewModel.DeleteItems();
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
        public async Task FindDuplicates()
        {
            await _databaseInteractionViewModel.FindDuplicates();
        }

        [RelayCommand]
        public async Task FindUnusedFiles()
        {
            await _databaseInteractionViewModel.FindUnusedFiles();
        }

        [RelayCommand]
        public async Task DeleteFile()
        {
            await _deleteItemCommandsViewModel.DeleteFiles();
        }

        [RelayCommand]
        public void ApplyChanges()
        {
            _changesViewModel.ApplyChanges();
        }

        [RelayCommand]
        public void CancelChanges()
        {
            _changesViewModel.CancelChanges();
        }
    }
}
