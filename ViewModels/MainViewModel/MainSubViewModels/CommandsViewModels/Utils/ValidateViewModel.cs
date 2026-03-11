using DatabaseTask.Models.AppData;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Interfaces;
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
        private readonly ICommandsHistory _commandsHistory;
        private readonly ConnectionStringData _connectionStringData;

        public ValidateViewModel(IMessageBoxService messageBoxService,
            ITreeViewFunctionality treeViewFunctionality,
            ICommandsHistory commandsHistory,
            ConnectionStringData connectionStringData)
            : base(messageBoxService)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _commandsHistory = commandsHistory;
            _connectionStringData = connectionStringData;
        }

        public async Task<bool> ValidateChanges()
        {

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
            
            if (_commandsHistory.GetItemsCommandsCount() is not 0)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Обнаружены операции с каталогами. Примите изменения или отмените их.",
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
