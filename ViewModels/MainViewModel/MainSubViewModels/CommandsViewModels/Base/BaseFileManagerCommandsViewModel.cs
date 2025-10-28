using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.Base;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base
{
    public class BaseFileManagerCommandsViewModel : ViewModelMessageBox
    {
        private readonly ICommandsFactory _itemCommandsFactory;
        private readonly IFileCommandsFactory _fileCommandsFactory;
        private readonly ICommandsHistory _commandsHistory;
        private readonly IFullPath _fullPath;

        public ICommandsFactory ItemCommandsFactory
        {
            get => _itemCommandsFactory;
        }
        
        public BaseFileManagerCommandsViewModel(
            IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath)
            : base(messageBoxService)
        { 
            _itemCommandsFactory = itemCommandsFactory;
            _fileCommandsFactory = fileCommandsFactory;
            _commandsHistory = commandsHistory;
            _fullPath = fullPath;
        }
        
        protected async Task ProcessCommand(CommandInfo commandInfo, LoggerDTO loggerDto)
        {
            await ExecuteCommand(commandInfo, loggerDto);
            //AddCommandToHistory(commandInfo);
        }

        private async Task ExecuteCommand(CommandInfo commandInfo, LoggerDTO loggerDto)
        {
            ICommand command = _itemCommandsFactory.CreateCommand(commandInfo,
                loggerDto);
            await command.Execute();
        }

        private void AddCommandToHistory(CommandInfo commandInfo)
        {
            string? data = null;
            if (commandInfo.Data != null)
            {
                data = _fullPath.GetFullpath(commandInfo.Data.ToString()!);
            }
            ICommand command = _fileCommandsFactory.CreateCommand(
                new CommandInfo(commandInfo.CommandType, data));
            _commandsHistory.AddCommand(command);
        }
    }
}
