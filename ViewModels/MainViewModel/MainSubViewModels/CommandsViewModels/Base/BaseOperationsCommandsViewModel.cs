using Avalonia.Platform;
using DatabaseTask.Models.Categories;
using DatabaseTask.Models.DTO;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations.Enums;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base
{
    public class BaseOperationsCommandsViewModel : BaseFileManagerCommandsViewModel, IBaseOperationsCommandsViewModel
    {
        private readonly IFullPath _fullPathService;
        //private readonly ITreeViewFunctionality _treeViewFunctionality;

        public BaseOperationsCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory, IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory, IFullPath fullPath
            ) 
            : base(messageBoxService, itemCommandsFactory, fileCommandsFactory, commandsHistory)
        {
            _fullPathService = fullPath;
            //_treeViewFunctionality = treeViewFunctionality;
        }

        public async Task CreateFolderOperation(INode node, string name)
        {
            CommandInfo itemInfo = new CommandInfo
            (
                CommandType.CreateFolder, name
            );

            string[] paths = GetPathForCommand(new List<(INode, string)>() { (node, name) },
                new List<PathEnum> { PathEnum.SelectedNodeWithNew });

            CommandInfo info = new CommandInfo
            (
                CommandType.CreateFolder, paths
            );

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

                string[] paths = GetPathForCommand(new List<(INode, string)>() { (node, "") },
                    new List<PathEnum> { PathEnum.SelectedNodeWithNew });

                CommandInfo commandInfo = new CommandInfo
                (
                    type, paths
                );

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
            //bool isFolder = false;
            //if (node is NodeViewModel newNode)
            //{
            //    isFolder = newNode.IsFolder;
            //}

            //CommandType type = isFolder == true ?
            //      CommandType.CopyFolder : CommandType.CopyFile;

            //CommandInfo itemInfo = new CommandInfo
            //(
            //    type, node,
            //    target,
            //    name
            //);

            //string[] paths = GetPathForCommand(new Dictionary<INode, string>() { { node, "" } },
            //        new List<PathEnum> { PathEnum.SelectedNodeWithNew });

            //CommandInfo commandInfo = new CommandInfo
            //(
            //    type, node.Name, Path.Combine(target.Name, node.Name)
            //);

            //GetPathForCommand(commandInfo, new List<PathEnum> { PathEnum.SelectedNode, PathEnum.NewNode });

            //await ProcessCommand(itemInfo, commandInfo,
            //   new LoggerDTO
            //   (
            //       isFolder ? LogCategory.CopyFolderCategory : LogCategory.CopyFileCategory,
            //       name ?? node.Name,
            //       target.Name
            //   )
            //);
        }

        public async Task MoveItemOperation(INode node, INode target, string name)
        {
            //bool isFolder = false;
            //if (node is NodeViewModel newNode) 
            //{
            //    isFolder = newNode.IsFolder;
            //}

            //CommandInfo itemInfo = new CommandInfo
            //(
            //    CommandType.MoveFile, node,
            //    target,
            //    name
            //);

            //CommandInfo commandInfo = new CommandInfo
            //(
            //    CommandType.MoveFile,
            //    node.Name, Path.Combine(target.Name, node.Name)
            //);

            //GetPathForCommand(commandInfo, new List<PathEnum> { PathEnum.SelectedNode, PathEnum.NewNode });

            //await ProcessCommand(itemInfo, commandInfo,
            //   new LoggerDTO
            //   (
            //       (isFolder) ? LogCategory.MoveCatalogCategory
            //       : LogCategory.MoveFileCategory,
            //       name ?? node.Name,
            //       target.Name
            //   )
            //);
        }

        public async Task RenameFolderOperation(INode node, string newName)
        {
            CommandInfo itemInfo = new CommandInfo
            (
              CommandType.RenameFolder, node.Name, newName
            );

            string[] paths = GetPathForCommand(new List<(INode, string)>() { (node, ""), (node.Parent!, newName) },
                new List<PathEnum> { PathEnum.SelectedNodeWithNew });

            CommandInfo info = new CommandInfo
            (
                CommandType.RenameFolder, paths
            );

            await ProcessCommand(itemInfo, info,
                new LoggerDTO
                (
                    LogCategory.RenameFolderCategory,
                    node.Name,
                    newName
                )
            );
        }

        private string[] GetPathForCommand(List<(INode node, string newName)> nodesPaths, List<PathEnum> paths)
        {
            List<string> fullPaths = new List<string>();

            foreach (var path in nodesPaths)
            {
                fullPaths.Add(_fullPathService.GetPathForNewItem(path.node, path.newName));
                //switch (paths[i])
                //{
                //    case PathEnum.SelectedNode:
                //        fullPaths.Add(_fullPathService.GetPathForExistedItem());
                //        break;
                //    //case PathEnum.SelectedNodeWithNew:
                //    //    fullPaths.Add(_fullPathService.GetPathForExistedItemWithNewItem(stringData));
                //        //break;
                //    case PathEnum.NewNode:
                //        fullPaths.Add(_fullPathService.GetPathForNewItem(nodesPaths[i].));
                //        break;
                //}            
            }

            return fullPaths.ToArray();
        }
    }
}
