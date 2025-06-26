using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IStorageService _storageService;

        public MainWindowViewModel(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [RelayCommand]
        public async Task OpenDb()
        {
            await ChooseDbFile();
        }

        public async Task<IEnumerable<IStorageFile>> ChooseDbFile()
        {
            try
            {
                return await _storageService.OpenFilesAsync(this,
                    new FilePickerOptions(StorageConstants.DbChose,
                    StorageConstants.DatabaseFilter));
            }
            catch (ArgumentNullException ex)
            {

            }
            catch (ArgumentException ex)
            {

            }
            catch (InvalidOperationException ex)
            {

            }
            return null;
        }
    }
}
