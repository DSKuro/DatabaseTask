using DatabaseTask.Models.Categories;
using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base
{
    public class BaseOperationsCommandsViewModel : BaseFileManagerCommandsViewModel, IBaseOperationsCommandsViewModel
    {
        private readonly IFullPath _fullPathService;

        public BaseOperationsCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory, IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory, IFullPath fullPath) 
            : base(messageBoxService, itemCommandsFactory, fileCommandsFactory, commandsHistory)
        {
            _fullPathService = fullPath;
        }

        public async Task CreateFolderOperation(string name)
        {
            CommandInfo itemInfo = new CommandInfo
            (
                CommandType.CreateFolder, name
            );

            CommandInfo info = new CommandInfo
            (
                CommandType.CreateFolder, name
            );

            GetPathForCommand(info);

            await ProcessCommand(itemInfo, info,
                new LoggerDTO
                (
                    LogCategory.CreateFolderCategory,
                    name.ToString()!
                )
            );
        }

        public async Task DeleteItemOperation(INode node, LogCategory category)
        {
            if (node is NodeViewModel nodeViewModel)
            {
                CommandType type = nodeViewModel.IsFolder == true ?
                    CommandType.DeleteFolder : CommandType.DeleteFile;

                CommandInfo itemInfo = new CommandInfo
                (
                    type, node
                );

                CommandInfo commandInfo = new CommandInfo
                (
                    type, node.Name
                );

                GetPathForCommand(commandInfo);

                await ProcessCommand(itemInfo, commandInfo,
                    new LoggerDTO
                    (
                        category,
                        node.Name
                    )
                );
            }
        }

        public async Task CopyItemOperation(INode node, INode target, string name)
        {
            bool isFolder = false;
            if (node is NodeViewModel newNode)
            {
                isFolder = newNode.IsFolder;
            }

            CommandType type = isFolder == true ?
                  CommandType.CopyFolder : CommandType.CopyFile;

            CommandInfo itemInfo = new CommandInfo
            (
                type, node,
                target,
                name
            );

            CommandInfo commandInfo = new CommandInfo
            (
                type, node.Name, Path.Combine(target.Name, node.Name)
            );

            GetPathForCommand(commandInfo);

            await ProcessCommand(itemInfo, commandInfo,
               new LoggerDTO
               (
                   isFolder ? LogCategory.CopyFolderCategory : LogCategory.CopyFileCategory,
                   name ?? node.Name,
                   target.Name
               )
            );
        }

        public async Task MoveItemOperation(INode node, INode target, string name)
        {
            bool isFolder = false;
            if (node is NodeViewModel newNode) 
            {
                isFolder = newNode.IsFolder;
            }

            CommandInfo itemInfo = new CommandInfo
            (
                CommandType.MoveFile, node,
                target,
                name
            );

            CommandInfo commandInfo = new CommandInfo
            (
                CommandType.MoveFile,
                node.Name, Path.Combine(target.Name, node.Name)
            );

            GetPathForCommand(commandInfo);

            await ProcessCommand(itemInfo, commandInfo,
               new LoggerDTO
               (
                   (isFolder) ? LogCategory.MoveCatalogCategory
                   : LogCategory.MoveFileCategory,
                   name ?? node.Name,
                   target.Name
               )
            );
        }

        public async Task RenameFolderOperation(string oldName, string newName)
        {
            CommandInfo itemInfo = new CommandInfo
            (
              CommandType.RenameFolder, oldName, newName
            );

            CommandInfo info = new CommandInfo
            (
                CommandType.RenameFolder, oldName, newName
            );

            GetPathForCommand(info);

            await ProcessCommand(itemInfo, info,
                new LoggerDTO
                (
                    LogCategory.RenameFolderCategory,
                    oldName,
                    newName
                )
            );
        }

        private void GetPathForCommand(CommandInfo commandInfo)
        {
            List<string> fullPaths = new List<string>();

            if (commandInfo.Data != null)
            {
                foreach (var data in commandInfo.Data)
                {
                    string? stringData = data as string;
                    if (stringData != null)
                    {
                        fullPaths.Add(_fullPathService.GetFullpath(stringData));
                    }
                }
            }

            commandInfo.Data = fullPaths.ToArray();
        }
    }
}
