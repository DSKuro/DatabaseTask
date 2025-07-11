using DatabaseTask.Models;
using DatabaseTask.Services.Collection;
using DatabaseTask.Services.Commands.Enum;
using DatabaseTask.Services.Commands.Info;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.FileManagerOperations.Exceptions;
using MsBox.Avalonia.Enums;
using System;
using System.Linq;
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
            LogCategory category, bool isFirst,
            params object[] parameters)
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
            ProcessCommandImpl(await getData.Invoke(), type, category, isFirst, parameters);
        }
        
        private void ProcessCommandImpl(object? data, CommandType type,
            LogCategory category,
            bool isFirst,
            params object[] parameters)
        {
            if (CanExecuteCommand(data))
            {
                object[] newParameters;
                if (data == null || data is ButtonResult)
                {
                    newParameters = parameters;
                }
                else
                {
                    if (isFirst)
                    {
                        newParameters = new object[] { data }.Concat(parameters).ToArray();
                    }
                    else
                    {
                        newParameters = new object[parameters.Length + 1];
                        Array.Copy(parameters, newParameters, parameters.Length);
                        newParameters[parameters.Length] = data;
                    }
                }
                ExecuteCommand(data, type,
                    category,
                    newParameters);
            }
        }

        protected virtual bool CanExecuteCommand(object? data)
        {
            return true;
        }

        private void ExecuteCommand(object? data, CommandType type, LogCategory category,
            object[] parameters)
        {
            ICommand command = _itemCommandsFactory.CreateCommand(new CommandInfo(data, type),
                new LoggerDTO(category, parameters));
            command.Execute();
        }
    }
}
