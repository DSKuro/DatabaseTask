using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.FilePicker;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IFilePickerService _filePickerService;

        public MainWindowViewModel(IFilePickerService filePickerService)
        {
            _filePickerService = filePickerService;
        }

        [RelayCommand]
        public async Task OpenDb()
        {
            await ChooseDbFile();
        }

        public async Task<IEnumerable<IStorageFile>> ChooseDbFile()
        {
           return await _filePickerService.OpenFilesAsync(this,
               new FilePickerOptions(FilePickerConstants.DbChose,
               FilePickerConstants.DatabaseFilter));
        }
    }
}
