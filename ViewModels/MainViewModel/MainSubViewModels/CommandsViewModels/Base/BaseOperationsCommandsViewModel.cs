using DatabaseTask.Models.Categories;
using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base
{
    public class BaseOperationsCommandsViewModel : BaseFileManagerCommandsViewModel, IBaseOperationsCommandsViewModel
    {
        private readonly IFullPath _fullPathService;

        public BaseOperationsCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory, IFileCommandsFactory fileCommandsFactory,
            IDatabaseCommandsFactory databaseCommandsFactory,
            ICommandsHistory commandsHistory, IFullPath fullPath
            ) 
            : base(messageBoxService, itemCommandsFactory, fileCommandsFactory,
                  databaseCommandsFactory, commandsHistory)
        {
            _fullPathService = fullPath;
        }

        public async Task CreateFolderOperation(INode node, string name)
        {
            CommandInfo itemInfo = new CommandInfo
            (
                CommandType.CreateFolder, name
            );

            string[] paths = GetPathForCommand(new List<(INode, string)>() { (node, name) });

            CommandInfo info = new CommandInfo
            (
                CommandType.CreateFolder, paths
            );

            await ProcessCommand(itemInfo, info, null,
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

                var pathsNode = new List<(INode, string)>() { (node, "") };

                string[] paths = GetPathForCommand(pathsNode);

                CommandInfo commandInfo = new CommandInfo
                (
                    type, paths
                );

                string[] relativePaths = GetRelativePathForCommand(pathsNode);

                CommandInfo databaseInfo = new CommandInfo
                (
                    type, relativePaths
                );

                await ProcessCommand(itemInfo, commandInfo, databaseInfo,
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

            var pathsNode = new List<(INode, string)>() { (node, ""), (target, name) };

            string[] paths = GetPathForCommand(pathsNode);

            CommandInfo commandInfo = new CommandInfo
            (
                type, paths
            );

            string[] relativePaths = GetRelativePathForCommand(pathsNode);

            CommandInfo databaseInfo = new CommandInfo
            (
                type, relativePaths
            );

            await ProcessCommand(itemInfo, commandInfo, databaseInfo,
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

            var pathsNode = new List<(INode, string)>() { (node, ""), (target, name) };

            string[] paths = GetPathForCommand(pathsNode);

            CommandInfo commandInfo = new CommandInfo
            (
                CommandType.MoveFile,
                paths
            );

            string[] relativePaths = GetRelativePathForCommand(pathsNode);

            CommandInfo databaseInfo = new CommandInfo
            (
                CommandType.RenameFolder, relativePaths
            );

            await ProcessCommand(itemInfo, commandInfo, databaseInfo,
               new LoggerDTO
               (
                   (isFolder) ? LogCategory.MoveCatalogCategory
                   : LogCategory.MoveFileCategory,
                   name ?? node.Name,
                   target.Name
               )
            );
        }

        public async Task RenameFolderOperation(INode node, string newName)
        {
            CommandInfo itemInfo = new CommandInfo
            (
              CommandType.RenameFolder, node.Name, newName
            );

            var pathsNode = new List<(INode, string)>() { (node, ""), (node.Parent!, newName) };

            string[] paths = GetPathForCommand(pathsNode);

            CommandInfo info = new CommandInfo
            (
                CommandType.RenameFolder, paths
            );

            string[] relativePaths = GetRelativePathForCommand(pathsNode);

            CommandInfo databaseInfo = new CommandInfo
            (
                CommandType.RenameFolder, relativePaths
            );

            await ProcessCommand(itemInfo, info, databaseInfo,
                new LoggerDTO
                (
                    LogCategory.RenameFolderCategory,
                    node.Name,
                    newName
                )
            );
        }

        private string[] GetPathForCommand(List<(INode node, string newName)> nodesPaths)
        {
            return nodesPaths
                   .Select(item => _fullPathService.GetPathForNewItem(item.node, item.newName))
                   .ToArray();
        }

        private string[] GetRelativePathForCommand(List<(INode node, string newName)> nodesPaths)
        {
            return nodesPaths
                   .Select(item => _fullPathService.GetRelativePath(item.node, item.newName))
                   .ToArray();
        }
    }
}
