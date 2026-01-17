using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class DatabaseInteractionViewModel : ViewModelMessageBox, IDatabaseInteractionViewModel
    {
        private readonly IValidateViewModel _validateViewModel;

        public DatabaseInteractionViewModel(
            IMessageBoxService messageBoxService,
            IValidateViewModel validateViewModel)
            : base(messageBoxService)
        {
            _validateViewModel = validateViewModel;
        }

        public async Task FindDuplicates()
        {
            if (!await _validateViewModel.ValidateCatalogAndDatabaseAsync())
            {
                return;
            }

            var result = await WeakReferenceMessenger.Default.Send<MainWindowDuplicatesFilesMessage>();
        }

        public async Task FindUnusedFiles()
        {
            if (!await _validateViewModel.ValidateCatalogAndDatabaseAsync())
            {
                return;
            }

            var result = await WeakReferenceMessenger.Default.Send<MainWindowUnusedFilesMessage>();
        }
    }
}
