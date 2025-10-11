using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.ItemCommands.Commands;
using DatabaseTask.Services.Commands.ItemCommands.Interfaces;
using DatabaseTask.Services.Commands.LogCommands;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Decorator;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

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
                        .Add<DeleteItemCommand>(
                        info.Data!
                        );
                    break;

                case CommandType.CopyItem:
                    builder
                      .Add<CopyItemCommand>(info.Data!);
                    break;

                case CommandType.MoveFile:
                    MoveOperationDecorator decorator = _serviceProvider.GetRequiredService<MoveOperationDecorator>();
                    object[]? parameters = info.Data!.Concat(new object[] { decorator }).ToArray();
                    builder
                     .Add<CopyItemCommand>(parameters);
                    break;

                default:
                    throw new ArgumentException("Тип команды задан неправильно");
            }
            return builder.Add<LogAddCommand>(data).Build();
        }
    }
}
