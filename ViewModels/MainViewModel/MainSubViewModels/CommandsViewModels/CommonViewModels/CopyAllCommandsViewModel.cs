using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
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
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels
{
    public class CopyAllCommandsViewModel : BaseOperationsCommandsViewModel, ICopyAllCommandsViewModel
    {
        private readonly IMoveFileCommandsViewModel _moveFileCommandsViewModel;
        private readonly ICopyFolderCommandsViewModel _copyFolderCommandsViewModel;
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly IFileManagerFileOperationsPermissions _filePermissions;
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public CopyAllCommandsViewModel(
            IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IMoveFileCommandsViewModel moveFileCommandsViewModel,
            ICopyFolderCommandsViewModel copyFolderCommandsViewModel,
            IFileManagerFolderOperationsPermissions folderPermissions,
            IFileManagerFileOperationsPermissions filePermissions,
            ITreeViewFunctionality treeViewFunctionality)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath)
        {
            _moveFileCommandsViewModel = moveFileCommandsViewModel;
            _copyFolderCommandsViewModel = copyFolderCommandsViewModel;
            _folderPermissions = folderPermissions;
            _filePermissions = filePermissions;
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

                if (!nodes.Last().IsFolder)
                {
                    throw new FileManagerOperationsException("Вместо целевого каталога выбран файл");
                }

                if (foldersToDelete.Count < 2 && filesToDelete.Count < 2)
                {
                    throw new FileManagerOperationsException("Выбран один элемент");
                }

                if (filesToDelete.Any())
                {
                    await _moveFileCommandsViewModel.ExecuteOperation(filesToDelete, false, false);
                }

                if (foldersToDelete.Any())
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
