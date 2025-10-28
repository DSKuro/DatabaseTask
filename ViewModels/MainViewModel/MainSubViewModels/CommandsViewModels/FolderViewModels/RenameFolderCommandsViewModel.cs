using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces;
using MsBox.Avalonia.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels
{
    // лог неверный (move file -> MOVE DIRECTORY)
    public class RenameFolderCommandsViewModel : BaseOperationsCommandsViewModel, IRenameFolderCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly ITreeView _treeView;
        private readonly IMergeCommandsViewModel _mergeCommandsViewModel;

        public RenameFolderCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFolderOperationsPermissions folderPermissions,
            ITreeView treeView,
            IMergeCommandsViewModel mergeCommandsViewModel)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath)
        {
            _folderPermissions = folderPermissions;
            _treeView = treeView;
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
                await RenameFolderOperation((_treeView.SelectedNodes[0] as NodeViewModel)!.Name, name);
                return;
            }

            await ProcessDeepRename(name);
        }

        private async Task ProcessDeepRename(string name)
        {
            ButtonResult? result =
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                (ParametrizedMessageBoxCategory.RenameCatalogMergeMessageBox.Title,
                ParametrizedMessageBoxCategory.RenameCatalogMergeMessageBox.Content
                .GetStringWithParams(name,
                _treeView.SelectedNodes[0].Parent!.Name),
                ButtonEnum.YesNo));
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
                await _mergeCommandsViewModel.ProcessNodeRecursive(sourceChild, targetNode);
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
