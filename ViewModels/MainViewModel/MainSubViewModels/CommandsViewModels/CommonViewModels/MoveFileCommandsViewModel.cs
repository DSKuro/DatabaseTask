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
        private readonly IFileManagerFileOperationsPermissions _filePermissions;
        private readonly ITreeView _treeView;
        private readonly INameGenerator _generator;

        private bool _isMove;

        public MoveFileCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFileOperationsPermissions filePermissions,
            ITreeView treeView,
            INameGenerator generator)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath)
        {
            _filePermissions = filePermissions;
            _treeView = treeView;
            _generator = generator;
            _isMove = false;
        }

        public async Task CopyFile()
        {
            try
            {
                _isMove = false;
                await MoveFileImplementation();
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
                _isMove = true;
                await MoveFileImplementation();
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
        }

        private async Task MoveFileImplementation()
        {
            _filePermissions.CanCopyFile();
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
                await ProcessMoveForSingleFile(file, _treeView.SelectedNodes.Last());
            }
        }

        private async Task ProcessMoveForSingleFile(INode file, INode target)
        {
            //if (!_treeView.IsNodeExist(target, file.Name))
            //{
            //    await ProcessMoveCommand(file, target, file.Name);
            //    return;
            //}

            if (target == file.Parent)
            {
                if (_isMove)
                {
                    await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions(
                    ParametrizedMessageBoxCategory.MoveFileToParentMessageBox.Title,
                    ParametrizedMessageBoxCategory.MoveFileToParentMessageBox.Content
                    .GetStringWithParams(file.Name, target.Name), ButtonEnum.Ok));
                }
                else
                {
                    string newName = _generator.GenerateUniqueCopyName(target, file.Name);           
                    await ProcessMoveCommand(file, target, newName);
                }
                return;
            }

            await ProcessReplace(file, target);
        }


        private async Task ProcessReplace(INode file, INode target)
        {
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
            new MessageBoxOptions(
            ParametrizedMessageBoxCategory.MoveFileReplaceMessageBox.Title,
            ParametrizedMessageBoxCategory.MoveFileReplaceMessageBox.Content
            .GetStringWithParams(file.Name, target.Name),
            ButtonEnum.YesNo
            ));
            if (result != null && result == ButtonResult.Yes)
            {
                await ProcessReplaceImplementation(file, target);
            }
        }

        private async Task ProcessReplaceImplementation(INode file, INode target)
        {
            INode? node = target.Children.FirstOrDefault(x =>
                x.Name == file.Name);
            if (node != null)
            {
                await DeleteItemOperation(node, LogCategory.DeleteFileCategory);
                await ProcessMoveCommand(file, target, file.Name);
            }
        }

        private async Task ProcessMoveCommand(INode file, INode target, string newName)
        {
            if (_isMove)
            {
                await MoveItemOperation(file, target, newName);
                return;
            }
            await CopyItemOperation(file, target, newName);
        }
    }
}
