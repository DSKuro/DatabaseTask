using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels
{
    public class CopyAllCommandsViewModel : BaseOperationsCommandsViewModel, ICopyAllCommandsViewModel
    {
        private readonly IMoveFileCommandsViewModel _moveFileCommandsViewModel;
        private readonly ICopyFolderCommandsViewModel _copyFolderCommandsViewModel;
        private readonly IFileManagerCommonOperationsPermission _commonPermissions;
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public CopyAllCommandsViewModel(
            IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            IDatabaseCommandsFactory databaseCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IMoveFileCommandsViewModel moveFileCommandsViewModel,
            ICopyFolderCommandsViewModel copyFolderCommandsViewModel,
            IFileManagerCommonOperationsPermission commonPermissions,
            ITreeViewFunctionality treeViewFunctionality)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, databaseCommandsFactory, commandsHistory, fullPath)
        {
            _moveFileCommandsViewModel = moveFileCommandsViewModel;
            _copyFolderCommandsViewModel = copyFolderCommandsViewModel;
            _commonPermissions = commonPermissions;
            _treeViewFunctionality = treeViewFunctionality;
        }

        public async Task CopyAllItems()
        {
            try
            {
                List<NodeViewModel> nodes = _treeViewFunctionality.GetAllSelectedNodes()
                   .OfType<NodeViewModel>()
                   .ToList();

                List<INode> filesToDelete = nodes
                    .Where(x => !x.IsFolder)
                    .Cast<INode>()
                    .ToList();
                filesToDelete.Add(nodes.Last());

                List<INode> foldersToDelete = nodes
                    .Where(x => x.IsFolder)
                    .Cast<INode>()
                    .ToList();

                _commonPermissions.CanMoveItems(_treeViewFunctionality.GetAllSelectedNodes());

                if (filesToDelete.Count > 1)
                {
                    await _moveFileCommandsViewModel.ExecuteOperation(filesToDelete, false, true);
                }

                if (foldersToDelete.Count > 1)
                {
                    await _copyFolderCommandsViewModel.CopyFolderImplementation(foldersToDelete);
                }
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
        }
    }
}
