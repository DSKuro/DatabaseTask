using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels
{
    public class CopyFolderCommandViewModel : BaseFolderCommandsViewModel, ICopyFolderCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly ITreeView _treeView;

        public CopyFolderCommandViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFolderOperationsPermissions folderPermissions,
            ITreeView treeView)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath)
        {
            _folderPermissions = folderPermissions;
            _treeView = treeView;
        }

        public async Task CopyFolder()
        {
            try
            {
                await RenameFolderImplementation();
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                  (MessageBoxConstants.Error.Value, ex.Message,
                                  ButtonEnum.Ok), null);
            }
        }

        private async Task RenameFolderImplementation()
        {
            _folderPermissions.CanCopyCatalog();
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
              new MessageBoxOptions(
              MessageBoxCategory.MoveFileMessageBox.Title,
              MessageBoxCategory.MoveFileMessageBox.Content,
              ButtonEnum.YesNo
              ));
            if (result != null && result == ButtonResult.Yes)
            {
                await ProcessMove();
            }
        }

        private async Task ProcessMove()
        {
            List<INode> files = _treeView.SelectedNodes.SkipLast(1).ToList();
            foreach (INode file in files)
            {
                await ProcessRename(file, _treeView.SelectedNodes.Last());
            }
        }

        private async Task ProcessRename(INode file, INode target)
        {
            if (!_treeView.IsNodeExist(target, file.Name))
            {
                await ProcessCommand(new Models.DTO.CommandInfo
                        (
                            CommandType.CopyItem, file, target
                        ),
                        new Models.DTO.LoggerDTO
                        (
                            LogCategory.CopyFolderCategory,
                            file.Name,
                            target.Name
                        )
                );
                return;
            }

            await ProcessDeepRename(file, target);
        }

        private async Task ProcessDeepRename(INode file, INode target)
        {
            ButtonResult? result =
                await GetResultOfCatalogMessage(ParametrizedMessageBoxCategory.RenameCatalogMergeMessageBox.Content
                .GetStringWithParams(file.Name,
                target.Name));
            if (result != null && result == ButtonResult.Yes)
            {
                await ProcessDeepRenameImplementation(file, target);
            }
        }

        private async Task ProcessDeepRenameImplementation(INode child, INode targetNode)
        {
            INode sourceNode = child;
            List<INode> children = sourceNode.Children.ToList();

            foreach (INode sourceChild in children)
            {
                await ProcessNodeRecursive(sourceChild, targetNode);
            }
        }

        private async Task ProcessNodeRecursive(INode sourceChild, INode targetParent)
        {
            INode? existingChild = targetParent.Children.FirstOrDefault(x => x.Name == sourceChild.Name);

            if (existingChild == null)
            {
                await MoveFile(sourceChild, targetParent);
            }
            else
            {
                await ProcessMerge(sourceChild, targetParent, existingChild);
            }
        }

        private async Task ProcessMerge(INode sourceChild, INode targetParent, INode existingChild)
        {
            if (sourceChild is NodeViewModel sourceNode && existingChild is NodeViewModel existChild)
            {
                if (sourceNode.IsFolder && existChild.IsFolder)
                {
                    await ProcessCatalogMerge(sourceNode, existChild);
                }
                else
                {
                    await ProcessFilesMerge(sourceNode, targetParent);
                }
            }
        }

        private async Task ProcessCatalogMerge(INode sourceChild, INode existingChild)
        {
            ButtonResult? result = await GetResultOfCatalogMessage(
                ParametrizedMessageBoxCategory.RenameCatalogMergeMessageBox.Content
                .GetStringWithParams(sourceChild.Name, existingChild.Parent!.Name));
            if (result != null && result == ButtonResult.Yes)
            {
                foreach (INode nestedChild in sourceChild.Children.ToList())
                {
                    await ProcessNodeRecursive(nestedChild, existingChild);
                }
            }
        }

        private async Task ProcessFilesMerge(INode sourceChild, INode targetParent)
        {
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                  (ParametrizedMessageBoxCategory.RenameFileMergeMessageBox.Title,
                                  ParametrizedMessageBoxCategory.RenameFileMergeMessageBox.Content
                                  .GetStringWithParams(sourceChild.Name, targetParent.Name),
                                  ButtonEnum.YesNo));
            if (result != null && result == ButtonResult.Yes)
            {
                await DeleteFolder(targetParent.Children.FirstOrDefault(x => x.Name == sourceChild.Name)!);
                await MoveFile(sourceChild, targetParent);
            }
        }

        private async Task<ButtonResult?> GetResultOfCatalogMessage(string content)
        {
            return await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                (ParametrizedMessageBoxCategory.RenameCatalogMergeMessageBox.Title,
                content,
                ButtonEnum.YesNo));
        }

        private async Task DeleteFolder(INode node)
        {
            if (node.Children.Count == 0)
            {
                await ProcessCommand(new Models.DTO.CommandInfo
                    (
                        CommandType.DeleteItem, node
                    ),
                    new Models.DTO.LoggerDTO
                    (
                        LogCategory.DeleteFolderCategory,
                        node.Name
                    )
                );
            }
        }

        private async Task MoveFile(INode sourceChild, INode targetParent)
        {
            await ProcessCommand(new Models.DTO.CommandInfo
                (
                    CommandType.CopyItem, sourceChild, targetParent
                ),
                new Models.DTO.LoggerDTO
                (
                    LogCategory.CopyFileCategory,
                    sourceChild.Name,
                    targetParent.Name
                )
            );
        }
    }
}
