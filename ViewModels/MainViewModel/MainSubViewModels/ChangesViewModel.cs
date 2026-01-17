using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Operations.LoggerOperations.Interfaces;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class ChangesViewModel : ViewModelBase, IChangesViewModel
    {
        private readonly ICommandsHistory _commandsHistory;
        private readonly ILoggerOperations _loggerOperations;

        public ChangesViewModel(ICommandsHistory commandsHistory, 
            ILoggerOperations loggerOperations)
        {
            _commandsHistory = commandsHistory;
            _loggerOperations = loggerOperations;
        }

        public void ApplyChanges()
        {
            List<bool> results = _commandsHistory.ExecuteAllCommands();
            if (results.Any())
            {
                _loggerOperations.UpdateStatus(results);
            }
        }

        public void CancelChanges()
        {
            _commandsHistory.ClearAll();
            _loggerOperations.ClearAll();
            _commandsHistory.ExecuteUndoItemsCommands();
        }
    }
}
