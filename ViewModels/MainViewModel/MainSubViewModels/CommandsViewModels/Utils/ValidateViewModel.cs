using DatabaseTask.Models.AppData;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils
{
    public class ValidateViewModel : ViewModelMessageBox, IValidateViewModel
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly ConnectionStringData _connectionStringData;

        public ValidateViewModel(IMessageBoxService messageBoxService,
            ITreeViewFunctionality treeViewFunctionality,
            ConnectionStringData connectionStringData)
            : base(messageBoxService)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _connectionStringData = connectionStringData;
        }

        public async Task<bool> ValidateCatalogAndDatabaseAsync()
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
