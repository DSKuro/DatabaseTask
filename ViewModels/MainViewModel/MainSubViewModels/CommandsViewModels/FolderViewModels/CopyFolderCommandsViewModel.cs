using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.Operations.Utils.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
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
    public class CopyFolderCommandViewModel : BaseOperationsCommandsViewModel, ICopyFolderCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly IFileManagerCommonOperationsPermission _commonPermissions;
        private readonly INameGenerator _generator;
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IMergeCommandsViewModel _mergeCommandsViewModel;

        public CopyFolderCommandViewModel(IMessageBoxService messageBoxService,
            ILoggerCommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            IDatabaseCommandsFactory databaseCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFolderOperationsPermissions folderPermissions,
            IFileManagerCommonOperationsPermission commonPermissions,
            INameGenerator generator,
            ITreeViewFunctionality treeViewFunctionality,
            IMergeCommandsViewModel mergeCommandsViewModel)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, databaseCommandsFactory, commandsHistory, fullPath)
        {
            _folderPermissions = folderPermissions;
            _commonPermissions = commonPermissions;
            _generator = generator;
            _treeViewFunctionality = treeViewFunctionality;
            _mergeCommandsViewModel = mergeCommandsViewModel;
        }

        public async Task CopyFolder()
        {
            try
            {
                _commonPermissions.CanMoveItems(_treeViewFunctionality.GetAllSelectedNodes());
                await CopyFolderImplementation(_treeViewFunctionality.GetAllSelectedNodes());
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                  (MessageBoxConstants.Error.Value, ex.Message,
                                  ButtonEnum.Ok), null);
            }
        }

        public async Task CopyFolderImplementation(List<INode> nodes)
        {
            _folderPermissions.CanCopyCatalog(nodes);
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
              new MessageBoxOptions(
              MessageBoxCategory.CopyFolderMessageBox.Title,
              MessageBoxCategory.CopyFolderMessageBox.Content,
              ButtonEnum.YesNo
              ));
            if (result != null && result == ButtonResult.Yes)
            {
                await ProcessMove(nodes);
            }
        }

        private async Task ProcessMove(List<INode> nodes)
        {
            List<INode> files = nodes.SkipLast(1).ToList();
            foreach (INode file in files)
            {
                await ProcessCopy(file, nodes.Last());
            }
        }

        private async Task ProcessCopy(INode file, INode target)
        {
            if (!_treeViewFunctionality.IsNodeExist(target, file.Name))
            {
                await CopyItemOperation(file, target, file.Name);
                return;
            }

            if (target == file.Parent)
            {
                string newName = _generator.GenerateUniqueCopyName(target, file.Name);
                await CopyItemOperation(file, target, newName);
                return;
            }

            await _mergeCommandsViewModel.ProcessNodeRecursive(file, target, false);
        }
    }
}
