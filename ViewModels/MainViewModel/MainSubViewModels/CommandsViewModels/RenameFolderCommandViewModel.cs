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
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Interfaces;
using MsBox.Avalonia.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels
{
    public class RenameFolderCommandViewModel : BaseFolderCommandsViewModel, IRenameFolderCommandsViewModel
    {
        
        private readonly IFileManagerFolderOperationsPermissions _folderPermissions;
        private readonly ITreeView _treeView;

        public RenameFolderCommandViewModel(IMessageBoxService messageBoxService,
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

            ButtonResult? result = await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                  (MessageBoxConstants.Error.Value, "TEST",
                                  ButtonEnum.YesNo));
            if (result != null && result == ButtonResult.Yes)
            {
                INode node = _treeView.SelectedNodes[0]
                    .Parent
                    .Children
                    .FirstOrDefault(x => x.Name == name);
                await ProcessDeepRename(node);
            }
        }

        private async Task ProcessDeepRename(INode targetNode)
        {
            INode sourceNode = _treeView.SelectedNodes[0];

            foreach (var sourceChild in sourceNode.Children.ToList())
            {
                await ProcessNodeRecursive(sourceChild, targetNode);
            }

            if (sourceNode.Children.Count == 0)
            {
                await ProcessCommand(new Models.DTO.CommandInfo
                (
                    CommandType.DeleteItem, new List<INode> { sourceNode }
                ),
                new Models.DTO.LoggerDTO
                (
                    LogCategory.DeleteFolderCategory,
                    sourceNode.Name
                )
                );
            }
        }

        private async Task ProcessNodeRecursive(INode sourceChild, INode targetParent)
        {
            var existingChild = targetParent.Children.FirstOrDefault(x => x.Name == sourceChild.Name);

            if (existingChild == null)
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
            else
            {
                if ((sourceChild as NodeViewModel).IsFolder && (existingChild as NodeViewModel).IsFolder)
                {
                    ButtonResult? result = await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                  (MessageBoxConstants.Error.Value, "TEST",
                                  ButtonEnum.YesNo));
                    if (result != null && result == ButtonResult.Yes)
                    {
                        foreach (var nestedChild in sourceChild.Children.ToList())
                        {
                            await ProcessNodeRecursive(nestedChild, existingChild);
                        }

                        if (sourceChild.Children.Count == 0)
                        {
                            await ProcessCommand(new Models.DTO.CommandInfo
                            (
                                CommandType.DeleteItem, new List<INode> { sourceChild }
                            ),
                            new Models.DTO.LoggerDTO
                            (
                                LogCategory.DeleteFolderCategory,
                                sourceChild.Name
                            )
                            );
                        }
                    }
                    
                }
                else
                {
                    ButtonResult? result = await MessageBoxHelper("MainDialogueWindow", new MessageBoxOptions
                                  (MessageBoxConstants.Error.Value, "TEST",
                                  ButtonEnum.YesNo));
                    if (result != null && result == ButtonResult.Yes)
                    {
                        string newName = GenerateUniqueName(targetParent, sourceChild.Name);
                        sourceChild.Name = newName;
                        await ProcessCommand(new Models.DTO.CommandInfo
                            (
                                CommandType.MoveFile, sourceChild, targetParent
                            ),
                            new Models.DTO.LoggerDTO
                            (
                                LogCategory.MoveFileCategory,
                                sourceChild.Name,
                                targetParent.Name
                            ));
                    }
                }
            }
        }

        private string GenerateUniqueName(INode parent, string baseName)
        {
            string newName = baseName;
            int copyNumber = 1;

            while (parent.Children.Any(x => x.Name == newName))
            {
                newName = $"{baseName} ({copyNumber})";
                copyNumber++;
            }

            return newName;
        }
    }
}
