using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Info;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.Base;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels
{
    public class BaseFolderCommandsViewModel : ViewModelMessageBox
    {
        private readonly ICommandsFactory _itemCommandsFactory;
        private readonly IFileCommandsFactory _fileCommandsFactory;
        private readonly ICommandsHistory _commandsHistory;
        private readonly IFullPath _fullPath;

        public ICommandsFactory ItemCommandsFactory
        {
            get => _itemCommandsFactory;
        }
        
        public BaseFolderCommandsViewModel(
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
        
        protected void ProcessCommand(LoggerCommandDTO dto)
        {
            ExecuteCommand(dto);
            AddCommandToHistory(dto);
        }

        private void ExecuteCommand(LoggerCommandDTO dto)
        {
            ICommand command = _itemCommandsFactory.CreateCommand(new CommandInfo(dto.Data, dto.Type),
                new LoggerDTO(dto.Category, dto.Parameters));
            command.Execute();
        }

        private void AddCommandToHistory(LoggerCommandDTO dto)
        {
            string? data = null;
            if (dto.Data != null)
            {
                data = _fullPath.GetFullpath(dto.Data.ToString()!);
            }
            ICommand command = _fileCommandsFactory.CreateCommand(
                new CommandInfo(data, dto.Type));
            _commandsHistory.AddCommand(command);
        }
    }
}
