using DatabaseTask.Services.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseTask.Services.Commands
{
    public class FolderCommandsFactory : IFolderCommandsFactory
    {
        private IServiceProvider _serviceProvider;

        public FolderCommandsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommand CreateCreateFolderCommand(string folderName) 
        {
            return ActivatorUtilities.CreateInstance<CreateFolderCommand>(_serviceProvider, folderName);
        }
    }
}
