using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.TreeViewLogic.Functionality;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.Base;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base
{
    public class BaseFileManagerCommandsViewModel : ViewModelMessageBox
    {
        private readonly ILoggerCommandsFactory _itemCommandsFactory;
        private readonly IFileCommandsFactory _fileCommandsFactory;
        private readonly IDatabaseCommandsFactory _databaseCommandsFactory;
        private readonly ICommandsHistory _commandsHistory;
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public ILoggerCommandsFactory ItemCommandsFactory
        {
            get => _itemCommandsFactory;
        }
        
        public BaseFileManagerCommandsViewModel(
            IMessageBoxService messageBoxService,
            ILoggerCommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            IDatabaseCommandsFactory databaseCommandsFactory,
            ICommandsHistory commandsHistory,
            ITreeViewFunctionality treeViewFunctionality)
            : base(messageBoxService)
        { 
            _itemCommandsFactory = itemCommandsFactory;
            _fileCommandsFactory = fileCommandsFactory;
            _databaseCommandsFactory = databaseCommandsFactory;
            _commandsHistory = commandsHistory;
            _treeViewFunctionality = treeViewFunctionality;
        }

        protected async Task ProcessCommand(CommandInfo itemInfo, CommandInfo commandInfo,
            CommandInfo? databaseInfo, LoggerDTO loggerDto)
        {
            await ExecuteCommand(itemInfo, loggerDto);
            AddCommandToHistory(commandInfo);
            if (databaseInfo is not null)
            {
                AddDatabaseCommandToHistory(databaseInfo);
            }
        }

        private async Task ExecuteCommand(CommandInfo commandInfo, LoggerDTO loggerDto)
        {
            ICommand command = _itemCommandsFactory.CreateCommand(commandInfo,
                loggerDto);
            await command.Execute();
            _commandsHistory.AddItemCommand(command);
        }

        private void AddCommandToHistory(CommandInfo commandInfo)
        {
            IResultCommand command = _fileCommandsFactory.CreateCommand(commandInfo);
            _commandsHistory.AddCommand(command);
        }

        private void AddDatabaseCommandToHistory(CommandInfo info)
        {
            IResultCommand command = _databaseCommandsFactory.CreateCommand(info);
            _commandsHistory.AddDatabaseCommand(command);
        }
    }
}
