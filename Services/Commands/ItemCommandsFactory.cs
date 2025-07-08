using DatabaseTask.Services.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseTask.Services.Commands
{
    public class ItemCommandsFactory : IItemCommandsFactory
    {
        private IServiceProvider _serviceProvider;

        public ItemCommandsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommand CreateCreateFolderCommand(string folderName) 
        {
            return ActivatorUtilities.CreateInstance<CreateFolderCommand>(_serviceProvider, folderName);
        }

        public ICommand CreateRenameFolderCommand(string newName)
        {
            return ActivatorUtilities.CreateInstance<RenameFolderCommand>(_serviceProvider, newName);
        }
        
        public ICommand CreateDeleteItemCommand()
        {
            return ActivatorUtilities.CreateInstance<DeleteItemCommand>(_serviceProvider);
        }

        public ICommand CreateCopyFolderCommand(bool isCopy)
        {
            return ActivatorUtilities.CreateInstance<CopyFolderCommand>(_serviceProvider, isCopy);
        }
    }
}
