using DatabaseTask.Models;
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
        private readonly IItemCommandsFactory _itemCommandsFactory;

        public IItemCommandsFactory ItemCommandsFactory
        {
            get => _itemCommandsFactory;
        }
        
        public BaseFolderCommandsViewModel(IMessageBoxService messageBoxService,
            IItemCommandsFactory itemCommandsFactory)
            : base(messageBoxService)
        { 
            _itemCommandsFactory = itemCommandsFactory;
        }

        protected async Task ProcessCommand(Action permission, Func<Task<object?>> getData, CommandType type)
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
            ProcessCommandImpl(await getData.Invoke(), type);
        }
        
        private void ProcessCommandImpl(object? data, CommandType type)
        {
            if (CanExecuteCommand(data))
            {
                ExecuteCommand(data, type);
            }
        }

        protected virtual bool CanExecuteCommand(object? data)
        {
            return true;
        }

        private void ExecuteCommand(object? data, CommandType type)
        {
            ICommand command = _itemCommandsFactory.CreateCommand(new CommandInfo(data, type));
            command.Execute();
        }
    }
}
