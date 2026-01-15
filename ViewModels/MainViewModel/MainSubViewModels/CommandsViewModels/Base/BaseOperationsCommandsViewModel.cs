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
            ILoggerCommandsFactory itemCommandsFactory, IFileCommandsFactory fileCommandsFactory,
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
            var targets = new List<(INode, string)>() { (node, name) };
            var loggerDto = new LoggerDTO(LogCategory.CreateFolderCategory, name.ToString()!);
            await ExecuteFileSystemOperation(CommandType.CreateFolder, targets,
                new object[] { name }, loggerDto);
        }

        public async Task DeleteItemOperation(INode node, LogCategory category)
        {
            if (node is not NodeViewModel nodeViewModel)
            {
                return;
            }

            CommandType type = nodeViewModel.IsFolder == true ?
                    CommandType.DeleteFolder : CommandType.DeleteFile;
            var targets = new List<(INode, string)>() { (node, "") };
            var loggerDto = new LoggerDTO(category, node.Name);
            await ExecuteFileSystemOperation(type, targets,
                new object[] { node }, loggerDto);
        }

        public async Task CopyItemOperation(INode node, INode target, string name)
        {
            if (node is not NodeViewModel nodeViewModel)
            {
                return;
            }

            CommandType type = nodeViewModel.IsFolder == true ?
                    CommandType.CopyFolder : CommandType.CopyFile;
            var targets = new List<(INode, string)>() { (node, ""), (target, name) };
            var loggerDto = new LoggerDTO(nodeViewModel.IsFolder ? LogCategory.CopyFolderCategory : LogCategory.CopyFileCategory,
                name ?? node.Name, target.Name);
            await ExecuteFileSystemOperation(type, targets,
                new object[] { node, target, name! }, loggerDto);
        }

        public async Task MoveItemOperation(INode node, INode target, string name)
        {
            if (node is not NodeViewModel nodeViewModel)
            {
                return;
            }

            var targets = new List<(INode, string)>() { (node, ""), (target, name) };
            var loggerDto = new LoggerDTO((nodeViewModel.IsFolder) ? LogCategory.MoveCatalogCategory
                   : LogCategory.MoveFileCategory,
                   name ?? node.Name,
                   target.Name);
            await ExecuteFileSystemOperation(CommandType.MoveFile, targets,
                new object[] { node, target, name! }, loggerDto);
        }

        public async Task RenameFolderOperation(INode node, string newName)
        {
            var targets = new List<(INode, string)>() { (node, ""), (node.Parent!, newName) };
            var loggerDto = new LoggerDTO(LogCategory.RenameFolderCategory,
                    node.Name,
                    newName);
            await ExecuteFileSystemOperation(CommandType.RenameFolder, targets,
                new object[] { node.Name, newName }, loggerDto);
        }

        private async Task ExecuteFileSystemOperation(
                            CommandType type,
                            List<(INode node, string name)> targets,
                            object[] itemArgs,
                            LoggerDTO logger)
        {
            CommandInfo itemInfo = new CommandInfo(type, itemArgs);

            string[] paths = GetPathForCommand(targets);
            CommandInfo commandInfo = new CommandInfo(type, paths);

            string[] relativePaths = GetRelativePathForCommand(targets);
            CommandInfo databaseInfo = new CommandInfo(type, relativePaths);

            await ProcessCommand(itemInfo, commandInfo, databaseInfo, logger);
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
