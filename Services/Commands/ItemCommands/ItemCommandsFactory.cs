using DatabaseTask.Models;
using DatabaseTask.Services.Commands.Enum;
using DatabaseTask.Services.Commands.Info;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.LogCommands;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Decorator;
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
                    builder
                        .Add<CreateFolderItemCommand>(info.Data);
                    break;

                case CommandType.RenameFolder:
                    builder
                        .Add<RenameFolderItemCommand>(info.Data);
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
                    return null;
            }
            return builder.Add<LogAddCommand>(data).Build();
        }
    }
}
