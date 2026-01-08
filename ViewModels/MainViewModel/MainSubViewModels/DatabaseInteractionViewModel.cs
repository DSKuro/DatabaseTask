using DatabaseTask.Models.AppData;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class DatabaseInteractionViewModel : ViewModelMessageBox, IDatabaseInteractionViewModel
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly ConnectionStringData _connectionStringData;

        public DatabaseInteractionViewModel(
            IMessageBoxService messageBoxService,
            ITreeViewFunctionality treeViewFunctionality,
            ConnectionStringData connectionStringData)
            : base(messageBoxService)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _connectionStringData = connectionStringData;
        }

        public async Task FindDuplicates()
        {
            if (!await ValidateCatalogAndDatabaseAsync())
            {
                return;
            }
        }

        public async Task FindUnusedFiles()
        {
            if (!await ValidateCatalogAndDatabaseAsync())
            {
                return;
            }
        }

        private async Task<bool> ValidateCatalogAndDatabaseAsync()
        {
            if (IsCatalogAndDatabaseNotChosen())
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "База данных или каталог не выбраны",
                    ButtonEnum.Ok));
                return false;
            }
            return true;
        }

        private bool IsCatalogAndDatabaseNotChosen()
        {
            return string.IsNullOrWhiteSpace(_connectionStringData.ConnectionString)
                || _treeViewFunctionality.GetCoreNode() == null;
        }
    }
}
