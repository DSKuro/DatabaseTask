using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.Operations.Utils.Interfaces;
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
    public class RenameFolderCommandViewModel : BaseFolderCommandsViewModel, IRenameFolderCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly ITreeView _treeView;
        private readonly INameGenerator _generator;

        public RenameFolderCommandViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFolderOperationsPermissions folderPermissions,
            ITreeView treeView,
            INameGenerator generator)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath)
        {
            _folderPermissions = folderPermissions;
            _treeView = treeView;
            _generator = generator;
        }

        public async Task RenameFolder()
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
            _folderPermissions.CanDoOperationOnFolder();
            object? data = await WeakReferenceMessenger.Default.Send<MainWindowRenameFolderMessage>();
            string? name = data?.ToString();
            if (name == null)
            {
                return;
            }

            await ProcessRename(name);
        }

        private async Task ProcessRename(string name)
        {
            if (!_treeView.IsParentHasNodeWithName(_treeView.SelectedNodes[0], name))
            {
                await ProcessCommand(new Models.DTO.CommandInfo
                        (
                            CommandType.RenameFolder, name
                        ),
                        new Models.DTO.LoggerDTO
                        (
                            LogCategory.RenameFolderCategory,
                            (_treeView.SelectedNodes[0] as NodeViewModel)!.Name,
                            name
                        )
                );
                return;
            }

            await ProcessDeepRename(name);
        }

        private async Task ProcessDeepRename(string name)
        {
            ButtonResult? result =
                await GetResultOfCatalogMessage(ParametrizedMessageBoxCategory.RenameCatalogMergeMessageBox.Content
                .GetStringWithParams(name,
                _treeView.SelectedNodes[0].Parent!.Name));
            if (result != null && result == ButtonResult.Yes)
            {
                INode? parent = _treeView.SelectedNodes[0].Parent;
                if (parent != null)
                {
                    INode? node = parent.Children.FirstOrDefault(x => x.Name == name);
                    if (node != null)
                    {
                        await ProcessDeepRenameImplementation(node);
                    }
                }
            }
        }

        private async Task ProcessDeepRenameImplementation(INode targetNode)
        {
            INode sourceNode = _treeView.SelectedNodes[0];
            List<INode> children = sourceNode.Children.ToList();

            foreach (INode sourceChild in children)
            {
                await ProcessNodeRecursive(sourceChild, targetNode);
            }

            await DeleteFolder(sourceNode);
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

                await DeleteFolder(sourceChild);
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
                string newName = _generator.GenerateUniqueName(targetParent, sourceChild.Name);
                sourceChild.Name = newName;
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
                    CommandType.MoveFile, sourceChild, targetParent
                ),
                new Models.DTO.LoggerDTO
                (
                    LogCategory.MoveFileCategory,
                    sourceChild.Name,
                    targetParent.Name
                )
            );
        }
    }
}
