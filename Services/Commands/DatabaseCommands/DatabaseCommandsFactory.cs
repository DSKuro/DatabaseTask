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
                CommandType.RenameFolder => ActivatorUtilities.CreateInstance<RenameDatabaseCommand>(_serviceProvider, info.Data!),
                CommandType.DeleteFolder => ActivatorUtilities.CreateInstance<DeleteFolderCommand>(_serviceProvider, info.Data!),
                CommandType.DeleteFile => ActivatorUtilities.CreateInstance<DeleteFileCommand>(_serviceProvider, info.Data!),
                CommandType.CopyFolder => ActivatorUtilities.CreateInstance<CopyFolderCommand>(_serviceProvider, info.Data!),
                CommandType.CopyFile => ActivatorUtilities.CreateInstance<CopyFileCommand>(_serviceProvider, info.Data!),
                CommandType.MoveFile => ActivatorUtilities.CreateInstance<MoveFileCommand>(_serviceProvider, info.Data!),
                _ => throw new ArgumentException("Неверный тип команды")
            };
        }
    }
}
