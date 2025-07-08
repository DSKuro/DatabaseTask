using CommunityToolkit.Mvvm.Input;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.ViewModels.FileManager.Interfaces;
using DatabaseTask.ViewModels.Interfaces;
using DatabaseTask.ViewModels.Nodes;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public partial class MainWindowViewModel : ViewModelMessageBox
    {
        private readonly IFileManager _fileManager;

        private readonly IOpenDataViewModel _openDataViewModel;
        private readonly IFileManagerFolderCommandsViewModel _fileManagerFolderCommandsViewModel;

        public ObservableCollection<INode> Nodes { get; set;  }

        public IFileManager FileManager { get => _fileManager; }

        public MainWindowViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager,
            IOpenDataViewModel openDataViewModel,
            IFileManagerFolderCommandsViewModel fileManagerFolderCommandsViewModel) : base(messageBoxService)
        {
            _fileManager = fileManager;
            _openDataViewModel = openDataViewModel;
            _fileManagerFolderCommandsViewModel = fileManagerFolderCommandsViewModel;
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
            await _fileManagerFolderCommandsViewModel.CreateFolderImpl();
        }

        [RelayCommand]
        public async Task RenameFolder()
        {
            await _fileManagerFolderCommandsViewModel.RenameFolderImpl();
        }

        [RelayCommand]
        public async Task DeleteFolder()
        {
            await _fileManagerFolderCommandsViewModel.DeleteFolderImpl();
        }

        [RelayCommand]
        public async Task CopyFolder()
        {
            await _fileManagerFolderCommandsViewModel.CopyFolderImpl();
        }

        [RelayCommand]
        public async Task MoveFile()
        {
            await _fileManagerFolderCommandsViewModel.MoveFileImpl();
        }

        [RelayCommand]
        public async Task CopyFile()
        {
            await _fileManagerFolderCommandsViewModel.CopyFileImpl();
        }

        [RelayCommand]
        public async Task DeleteFile()
        {
            await _fileManagerFolderCommandsViewModel.DeleteFileImpl();
        }
    }
}
