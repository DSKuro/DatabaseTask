using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.ItemCommands.Commands;
using DatabaseTask.Services.Commands.ItemCommands.Interfaces;
using DatabaseTask.Services.Commands.LogCommands;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Decorator;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseTask.Services.Commands.ItemCommands
{
    public class ItemCommandsFactory : IItemCommandsFactory
    {
        private IServiceProvider _serviceProvider;

        public ItemCommandsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommand CreateCommand(CommandInfo info, LoggerDTO data)
        {
            ICompositeCommandBuilder builder = _serviceProvider.GetRequiredService<ICompositeCommandBuilder>();
            switch (info.CommandType) 
            {
                case CommandType.CreateFolder:
                    if (info.Data == null)
                    {
                        throw new ArgumentException("Данные команды не заданы");
                    }

                    builder
                        .Add<CreateFolderItemCommand>(info.Data!);
                    break;

                case CommandType.RenameFolder:
                    if (info.Data == null)
                    {
                        throw new ArgumentException("Данные команды не заданы");
                    }

                    builder
                        .Add<RenameFolderItemCommand>(info.Data!);
                    break;

                case CommandType.DeleteItem:
                    builder
                        .Add<DeleteItemCommand>();
                    break;

                case CommandType.CopyItem:
                    builder
                      .Add<CopyItemCommand>();
                    break;

                case CommandType.MoveFile:
                    builder
                     .Add<CopyItemCommand>(_serviceProvider.GetRequiredService<MoveOperationDecorator>());
                    break;

                default:
                    throw new ArgumentException("Тип команды задан неправильно");
            }
            return builder.Add<LogAddCommand>(data).Build();
        }
    }
}
