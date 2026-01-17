using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces;
using MsBox.Avalonia.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels
{
    public class RenameFolderCommandsViewModel : BaseOperationsCommandsViewModel, IRenameFolderCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IMergeCommandsViewModel _mergeCommandsViewModel;

        public RenameFolderCommandsViewModel(IMessageBoxService messageBoxService,
            ILoggerCommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            IDatabaseCommandsFactory databaseCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFolderOperationsPermissions folderPermissions,
            ITreeViewFunctionality treeViewFunctionality,
            IMergeCommandsViewModel mergeCommandsViewModel)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, databaseCommandsFactory, commandsHistory, fullPath,
                  treeViewFunctionality)
        {
            _folderPermissions = folderPermissions;
            _treeViewFunctionality = treeViewFunctionality;
            _mergeCommandsViewModel = mergeCommandsViewModel;
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
            _folderPermissions.CanRenameFolder();
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
            INode? selectedNode = _treeViewFunctionality.GetFirstSelectedNode();
            if (selectedNode != null)
            {
                if (!_treeViewFunctionality.IsParentHasNodeWithName(selectedNode, name))
                {
                    await RenameFolderOperation(selectedNode, name);
                    return;
                }

                await ProcessDeepRename(name, selectedNode);
            }
        }

        private async Task ProcessDeepRename(string name, INode selectedNode)
        {
            ButtonResult? result =
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                (ParametrizedMessageBoxCategory.RenameCatalogMergeMessageBox.Title,
                ParametrizedMessageBoxCategory.RenameCatalogMergeMessageBox.Content
                .GetStringWithParams(name,
                selectedNode.Parent!.Name),
                ButtonEnum.YesNo));
            if (result != null && result == ButtonResult.Yes)
            {
                INode? parent = selectedNode.Parent;
                if (parent != null)
                {
                    INode? node = _treeViewFunctionality.GetChildrenByName(parent, name);
                    if (node != null)
                    {
                        await ProcessDeepRenameImplementation(node, selectedNode);
                    }
                }
            }
        }

        private async Task ProcessDeepRenameImplementation(INode targetNode, INode selectedNode)
        {
            INode sourceNode = selectedNode;
            List<INode> children = sourceNode.Children.ToList();

            foreach (INode sourceChild in children)
            {
                await _mergeCommandsViewModel.ProcessNodeRecursive(sourceChild, targetNode, true);
            }

            await DeleteFolder(sourceNode);
        }

        private async Task DeleteFolder(INode node)
        {
            if (node.Children.Count == 0)
            {
                await DeleteItemOperation(node, LogCategory.DeleteFolderCategory);
            }
        }
    }
}
