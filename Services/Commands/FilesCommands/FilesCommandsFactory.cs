using DatabaseTask.Services.Commands.Enum;
using DatabaseTask.Services.Commands.Info;
using DatabaseTask.Services.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseTask.Services.Commands.FilesCommands
{
    public class FilesCommandsFactory : IFileCommandsFactory
    {
        private IServiceProvider _serviceProvider;

        public FilesCommandsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommand CreateCommand(CommandInfo info)
        {
            switch (info.CommandType)
            {
                case CommandType.CreateFolder:
                    return
                        ActivatorUtilities.CreateInstance<CreateFolderCommand>(_serviceProvider, info.Data);

                //    case CommandType.RenameFolder:
                //        return ActivatorUtilities.CreateInstance<RenameFolderItemCommand>(_serviceProvider, info.Data);

                //    case CommandType.DeleteItem:
                //        return ActivatorUtilities.CreateInstance<DeleteItemCommand>(_serviceProvider);

                //    case CommandType.CopyItem:
                //        return ActivatorUtilities.CreateInstance<CopyItemCommand>(_serviceProvider);

                //    case CommandType.MoveFile:
                //        return ActivatorUtilities.CreateInstance<CopyItemCommand>(_serviceProvider, _serviceProvider.GetRequiredService<MoveOperationDecorator>());
                default:
                    return null;
            }
        }
    }
}
