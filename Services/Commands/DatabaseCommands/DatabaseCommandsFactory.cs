using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Commands;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Commands;
using DatabaseTask.Services.Commands.Utility.Enum;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseTask.Services.Commands.DatabaseCommands
{
    public class DatabaseCommandsFactory : IDatabaseCommandsFactory
    {
        private IServiceProvider _serviceProvider;

        public DatabaseCommandsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IResultCommand CreateCommand(CommandInfo info)
        {
            if (info.Data == null)
            {
                throw new ArgumentException("Данные пустые");
            }

            return info.CommandType switch
            {
                CommandType.RenameFolder 
                or CommandType.MoveFile => ActivatorUtilities.CreateInstance<UpdateDatabaseCommand>(_serviceProvider, info.Data!),

                CommandType.CopyFolder
                or CommandType.CopyFile => ActivatorUtilities.CreateInstance<CopyDatabaseCommand>(_serviceProvider, info.Data!),

                CommandType.DeleteFolder => ActivatorUtilities.CreateInstance<DeleteFolderCommand>(_serviceProvider, info.Data!),
                CommandType.DeleteFile => ActivatorUtilities.CreateInstance<DeleteFileCommand>(_serviceProvider, info.Data!),
                _ => throw new ArgumentException("Неверный тип команды")
            };
        }
    }
}
