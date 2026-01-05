using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.ViewModels.Base;
using DatabaseTask.ViewModels.Logger.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class ApplyChangesViewModel : ViewModelBase, IApplyChangesViewModel
    {
        private readonly ICommandsHistory _commandsHistory;
        private readonly ILogger _logger;

        public ApplyChangesViewModel(ICommandsHistory commandsHistory, ILogger logger)
        {
            _commandsHistory = commandsHistory;
            _logger = logger;
        }

        public void ApplyChanges()
        {
            List<bool> results = _commandsHistory.ExecuteAllCommands();
            if (results.Any())
            {
                var logs = _logger.LogOperations;
                for (int i = 0; i < results.Count; i++)
                {
                    string path = results[i] == true ? StatusCategory.CorrectStatus.Path :
                        StatusCategory.WrongStatus.Path;
                    logs[i].ImagePath = path;
                }
            }
        }
    }
}
