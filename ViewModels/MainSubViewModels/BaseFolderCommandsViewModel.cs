using DatabaseTask.Models;
using DatabaseTask.Services.Commands.Info;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.LogCommands;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.FileManagerOperations.Exceptions;
using DatabaseTask.Services.FilesOperations.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia.Enums;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainSubViewModels
{
    public class BaseFolderCommandsViewModel : ViewModelMessageBox
    {
        private readonly ICommandsFactory _itemCommandsFactory;
        private readonly IFileCommandsFactory _fileCommandsFactory;
        private readonly ICommandsHistory _commandsHistory;
        private readonly IFullPath _fullPath;
        private readonly IServiceProvider _serviceProvider;

        public ICommandsFactory ItemCommandsFactory
        {
            get => _itemCommandsFactory;
        }
        
        public BaseFolderCommandsViewModel(
            IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IServiceProvider serviceProvider)
            : base(messageBoxService)
        { 
            _itemCommandsFactory = itemCommandsFactory;
            _fileCommandsFactory = fileCommandsFactory;
            _commandsHistory = commandsHistory;
            _fullPath = fullPath;
            _serviceProvider = serviceProvider;
        }

        protected async Task ProcessCommand(LoggerCommandDTO commandData)
        {
            try
            {
                commandData.Permission.Invoke();
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
            await ProcessCommandImpl(commandData);
        }
        
        private async Task ProcessCommandImpl(LoggerCommandDTO commandData)
        {
            object? data = await commandData.GetDataFunction.Invoke();
            if (CanExecuteCommand(data))
            {
                object[] newParameters;
                commandData.Parameters =
                    ActivatorUtilities.CreateInstance<GetParamsForLog>(_serviceProvider, data, commandData)
                    .GetParams();
                ExecuteCommand(data, commandData);
                AddCommandToHistory(data, commandData);
            }
        }

        protected virtual bool CanExecuteCommand(object? data)
        {
            return true;
        }

        private void ExecuteCommand(object? data, LoggerCommandDTO commandData)
        {
            ICommand command = _itemCommandsFactory.CreateCommand(new CommandInfo(data, commandData.Type),
                new LoggerDTO(commandData.Category, commandData.Parameters));
            command.Execute();
        }

        private void AddCommandToHistory(object? data, LoggerCommandDTO commandData)
        {
            ICommand command = _fileCommandsFactory.CreateCommand(new CommandInfo(_fullPath.GetFullpath((string)data), commandData.Type));
            _commandsHistory.AddCommand(command);
        }
    }
}
