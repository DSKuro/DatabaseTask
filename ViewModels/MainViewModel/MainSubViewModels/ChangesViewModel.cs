using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.LoggerOperations.Interfaces;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class ChangesViewModel : ViewModelMessageBox, IChangesViewModel
    {
        private const string _dialogueWindowName = "MainDialogueWindow";
        private readonly ICommandsHistory _commandsHistory;
        private readonly ILoggerOperations _loggerOperations;
        private readonly IValidateViewModel _validateViewModel;

        public ChangesViewModel(IMessageBoxService messageBoxService,
            ICommandsHistory commandsHistory, 
            ILoggerOperations loggerOperations,
            IValidateViewModel validateViewModel)
            : base(messageBoxService)
        {
            _commandsHistory = commandsHistory;
            _loggerOperations = loggerOperations;
            _validateViewModel = validateViewModel;
        }

        public async Task ApplyChanges()
        {
            if (!await _validateViewModel.ValidateCatalogAndDatabaseAsync())
            {
                return;
            }

            ButtonResult? result = await MessageBoxHelper(_dialogueWindowName,
                new MessageBoxOptions(MessageBoxCategory.ApplyChangesMessageBox.Title,
                MessageBoxCategory.ApplyChangesMessageBox.Content, ButtonEnum.YesNo));
            if (result is not null && result is ButtonResult.Yes)
            {
                List<bool> results = _commandsHistory.ExecuteAllCommands();
                if (results.Any())
                {
                    _loggerOperations.UpdateStatus(results);
                }
            }
        }

        public async Task CancelChanges()
        {
            ButtonResult? result = await MessageBoxHelper(_dialogueWindowName,
                new MessageBoxOptions(MessageBoxCategory.CancelChangesMessageBox.Title,
                MessageBoxCategory.CancelChangesMessageBox.Content, ButtonEnum.YesNo));
            if (result is not null && result is ButtonResult.Yes)
            {
                _commandsHistory.ClearAll();
                _commandsHistory.ExecuteUndoItemsCommands();
                _loggerOperations.ClearAll();
            }
        }
    }
}
