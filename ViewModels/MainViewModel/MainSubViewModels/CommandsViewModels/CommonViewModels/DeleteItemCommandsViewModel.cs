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
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels
{
    public class DeleteItemCommandsViewModel : BaseOperationsCommandsViewModel, IDeleteItemCommandsViewModel
    {
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly IFileManagerFileOperationsPermissions _filePermissions;
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public DeleteItemCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory,
            IFullPath fullPath,
            IFileManagerFolderOperationsPermissions folderPermissions,
            IFileManagerFileOperationsPermissions filePermissions,
            ITreeViewFunctionality treeViewFunctionality)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, commandsHistory, fullPath)
        {
            _folderPermissions = folderPermissions;
            _filePermissions = filePermissions;
            _treeViewFunctionality = treeViewFunctionality;
        }

        public async Task DeleteItems()
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

                List<INode> foldersToDelete = nodes
                    .Where(x => x.IsFolder)
                    .Cast<INode>()
                    .ToList();

                if (filesToDelete.Any())
                {
                    _filePermissions.CanDeleteFile(filesToDelete);
                }

                if (foldersToDelete.Any())
                {
                    _folderPermissions.CanDeleteFolder(foldersToDelete);
                }

                await ProcessDelete(
                    MessageBoxCategory.DeleteAllMessageBox.Title,
                    MessageBoxCategory.DeleteAllMessageBox.Content
                );
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
        }


        public async Task DeleteFiles()
        {
            try
            {
                _filePermissions.CanDeleteFile(_treeViewFunctionality.GetAllSelectedNodes());
                await ProcessDelete(MessageBoxCategory.DeleteFileMessageBox.Title,
                    MessageBoxCategory.DeleteFileMessageBox.Content);
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
        }

        public async Task DeleteFolders()
        {
            try
            {
                _folderPermissions.CanDeleteFolder(_treeViewFunctionality.GetAllSelectedNodes());
                await ProcessDelete(MessageBoxCategory.DeleteFolderMessageBox.Title,
                    MessageBoxCategory.DeleteFolderMessageBox.Content);
            }
            catch (FileManagerOperationsException ex)
            {
                await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                   (MessageBoxConstants.Error.Value, ex.Message,
                                   ButtonEnum.Ok), null);
            }
        }

        private async Task ProcessDelete(string title, string content)
        {
            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow",
                    new MessageBoxOptions(
                    title,
                    content,
                    ButtonEnum.YesNo));
            if (result != null && result == ButtonResult.Yes)
            {
                await ProcessDeleteCommand();
            }
        }

        private async Task ProcessDeleteCommand()
        {
            foreach (INode node in _treeViewFunctionality.GetAllSelectedNodes())
            {
                NodeViewModel? nodeViewModel = node as NodeViewModel;
                if (nodeViewModel != null)
                { 
                    LogCategory category = nodeViewModel.IsFolder == true ? 
                        LogCategory.DeleteFolderCategory : LogCategory.DeleteFileCategory;
                    await DeleteItemOperation(node, category);
                }
            }
        }
    }
}
