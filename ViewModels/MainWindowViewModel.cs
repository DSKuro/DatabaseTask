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
        private readonly IMessageBoxCommandsViewModel _messageBoxCommandsViewModel;
        private readonly IFolderCommandsViewModel _folderCommandsViewModel;

        public ObservableCollection<INode> Nodes { get; set;  }

        public IFileManager FileManager { get => _fileManager; }

        public MainWindowViewModel(IMessageBoxService messageBoxService,
            IFileManager fileManager,
            IOpenDataViewModel openDataViewModel,
            IMessageBoxCommandsViewModel messageBoxCommandsViewModel,
            IFolderCommandsViewModel folderCommandsViewModel) : base(messageBoxService)
        {
            _fileManager = fileManager;
            _openDataViewModel = openDataViewModel;
            _messageBoxCommandsViewModel = messageBoxCommandsViewModel;
            _folderCommandsViewModel = folderCommandsViewModel;
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
    }
}
