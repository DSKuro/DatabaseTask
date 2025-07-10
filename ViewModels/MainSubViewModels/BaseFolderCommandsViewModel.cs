using DatabaseTask.Models;
using DatabaseTask.Services.Collection;
using DatabaseTask.Services.Commands.Enum;
using DatabaseTask.Services.Commands.Info;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.FileManagerOperations.Exceptions;
using MsBox.Avalonia.Enums;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainSubViewModels
{
    public class BaseFolderCommandsViewModel : ViewModelMessageBox
    {
        private readonly ICommandsFactory _itemCommandsFactory;

        public ICommandsFactory ItemCommandsFactory
        {
            get => _itemCommandsFactory;
        }
        
        public BaseFolderCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory)
            : base(messageBoxService)
        { 
            _itemCommandsFactory = itemCommandsFactory;
        }

        protected async Task ProcessCommand(Action permission, Func<Task<object?>> getData, CommandType type,
            LogCategory category)
        {
            try
            {
                permission.Invoke();
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
            ProcessCommandImpl(await getData.Invoke(), type, category);
        }
        
        private void ProcessCommandImpl(object? data, CommandType type,
            LogCategory category)
        {
            if (CanExecuteCommand(data))
            {
                ExecuteCommand(data, type, category);
            }
        }

        protected virtual bool CanExecuteCommand(object? data)
        {
            return true;
        }

        private void ExecuteCommand(object? data, CommandType type, LogCategory category)
        {
            ICommand command = _itemCommandsFactory.CreateCommand(new CommandInfo(data, type),
                new LoggerDTO(category));
            command.Execute();
        }
    }
}
