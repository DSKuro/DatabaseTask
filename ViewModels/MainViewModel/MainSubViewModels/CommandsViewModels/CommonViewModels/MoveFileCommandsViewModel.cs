using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.Operations.Utils.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Operations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels
{
    public class MoveFileCommandsViewModel : BaseOperationsCommandsViewModel, IMoveFileCommandsViewModel
    {
        private readonly IFileManagerCommonOperationsPermission _permissions;
        private readonly IFileManagerFileOperationsPermissions _filePermissions;
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly ITreeView _treeView;
        private readonly INameGenerator _generator;
        private readonly INodeEvents _nodeEvents;

        private bool _isMove;

        public MoveFileCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerCommonOperationsPermission permissions,
            IFileManagerFileOperationsPermissions filePermissions,
            ITreeViewFunctionality treeViewFunctionality,
            ITreeView treeView,
            INameGenerator generator,
            INodeEvents nodeEvents)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath)
        {
            _permissions = permissions;
            _filePermissions = filePermissions;
            _treeViewFunctionality = treeViewFunctionality;
            _treeView = treeView;
            _generator = generator;
            _nodeEvents = nodeEvents;
            _isMove = false;
            _nodeEvents.OnDrop += ExecuteOperation;
        }

        public async Task CopyFile()
        {
            try
            {
                await ExecuteOperation(_treeView.SelectedNodes.ToList(), false, false);
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
        }

        public async Task MoveFile()
        {
            try
            {
                await ExecuteOperation(_treeView.SelectedNodes.ToList(), true, false);
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
        }

        public async Task ExecuteOperation(List<INode> nodes, bool isMove, bool allowFolders)
        {
            try
            {
                _isMove = isMove;
                await MoveFileImplementation(nodes, allowFolders);
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
        }

        private async Task MoveFileImplementation(List<INode> nodes, bool allowFolders = true)
        {
            ValidatePermissions(nodes, allowFolders);

            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
                new MessageBoxOptions(
                MessageBoxCategory.MoveFileMessageBox.Title,
                MessageBoxCategory.MoveFileMessageBox.Content,
                ButtonEnum.YesNo
                ));
            if (result != null && result == ButtonResult.Yes)
            {
                await ProcessMove(nodes);
            }
        }

        private void ValidatePermissions(List<INode> nodes, bool allowFolders)
        {
            if (allowFolders)
            {
                _permissions.MoveItems(nodes);
            }
            else
            {
                _filePermissions.CanCopyFile(nodes);
            }

        }

        private async Task ProcessMove(List<INode> nodes)
        {
            List<INode> items = nodes.SkipLast(1).ToList();
            INode target = nodes.Last();

            foreach (INode item in items)
            {
                await ProcessMoveForSingleItem(item, target);
            }
        }

        private async Task ProcessMoveForSingleItem(INode item, INode target)
        {
            if (!_treeViewFunctionality.IsNodeExist(target, item.Name))
            {
                await ProcessMoveCommand(item, target, item.Name);
                return;
            }

            if (target == item.Parent)
            {
                if (_isMove)
                {
                    await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    ParametrizedMessageBoxCategory.MoveFileToParentMessageBox.Title,
                    ParametrizedMessageBoxCategory.MoveFileToParentMessageBox.Content
                    .GetStringWithParams(item.Name, target.Name), ButtonEnum.Ok));
                }
                else
                {
                    string newName = _generator.GenerateUniqueCopyName(target, item.Name);
                    await ProcessMoveCommand(item, target, newName);
                }
                return;
            }

            await ProcessReplace(item, target);
        }

        private async Task ProcessReplace(INode item, INode target)
        {
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
            new MessageBoxOptions(
            ParametrizedMessageBoxCategory.MoveFileReplaceMessageBox.Title,
            ParametrizedMessageBoxCategory.MoveFileReplaceMessageBox.Content
            .GetStringWithParams(item.Name, target.Name),
            ButtonEnum.YesNo
            ));
            if (result != null && result == ButtonResult.Yes)
            {
                await ProcessReplaceImplementation(item, target);
            }
        }

        private async Task ProcessReplaceImplementation(INode item, INode target)
        {
            INode? existingNode = _treeViewFunctionality.GetChildrenByName(target, item.Name);
            if (existingNode != null)
            {
                var logCategory = IsFolder(existingNode)
                    ? LogCategory.DeleteFolderCategory
                    : LogCategory.DeleteFileCategory;

                await DeleteItemOperation(existingNode, logCategory);
                await ProcessMoveCommand(item, target, item.Name);
            }
        }

        private async Task ProcessMoveCommand(INode item, INode target, string newName)
        {
            if (_isMove)
            {
                await MoveItemOperation(item, target, newName);
                return;
            }
            await CopyItemOperation(item, target, newName);
        }

        private bool IsFolder(INode node)
        {
            return node is NodeViewModel nodeViewModel && nodeViewModel.IsFolder;
        }
    }
}
