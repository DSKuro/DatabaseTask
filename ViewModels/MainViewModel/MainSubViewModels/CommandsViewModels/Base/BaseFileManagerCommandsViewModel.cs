using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.ViewModels.Base;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base
{
    public class BaseFileManagerCommandsViewModel : ViewModelMessageBox
    {
        private readonly ICommandsFactory _itemCommandsFactory;
        private readonly IFileCommandsFactory _fileCommandsFactory;
        private readonly ICommandsHistory _commandsHistory;

        public ICommandsFactory ItemCommandsFactory
        {
            get => _itemCommandsFactory;
        }
        
        public BaseFileManagerCommandsViewModel(
            IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory)
            : base(messageBoxService)
        { 
            _itemCommandsFactory = itemCommandsFactory;
            _fileCommandsFactory = fileCommandsFactory;
            _commandsHistory = commandsHistory;
        }
        
        protected async Task ProcessCommand(CommandInfo itemInfo, CommandInfo commandInfo, LoggerDTO loggerDto)
        {
            await ExecuteCommand(itemInfo, loggerDto);
            AddCommandToHistory(commandInfo);
        }

        private async Task ExecuteCommand(CommandInfo commandInfo, LoggerDTO loggerDto)
        {
            ICommand command = _itemCommandsFactory.CreateCommand(commandInfo,
                loggerDto);
            await command.Execute();
        }

        private void AddCommandToHistory(CommandInfo commandInfo)
        {
            IResultCommand command = _fileCommandsFactory.CreateCommand(commandInfo);
            _commandsHistory.AddCommand(command);
        }
    }
}
