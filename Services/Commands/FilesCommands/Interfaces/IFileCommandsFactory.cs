﻿using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;

namespace DatabaseTask.Services.Commands.FilesCommands.Interfaces
{
    public interface IFileCommandsFactory
    {
        public ICommand CreateCommand(CommandInfo info);
    }
}
