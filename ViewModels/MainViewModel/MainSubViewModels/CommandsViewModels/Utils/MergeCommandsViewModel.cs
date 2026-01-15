using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.Operations.Utils.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces;
using MsBox.Avalonia.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils
{
    public class MergeCommandsViewModel : BaseOperationsCommandsViewModel, IMergeCommandsViewModel
    {
        private readonly INameGenerator _generator;
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        private bool _isMove;

        public MergeCommandsViewModel(IMessageBoxService messageBoxService,
            ILoggerCommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            IDatabaseCommandsFactory databaseCommandsFactory,
            ICommandsHistory commandsHistory, IFullPath fullPath,
            INameGenerator generator,
            ITreeViewFunctionality treeViewFunctionality)
            : base(messageBoxService, itemCommandsFactory,
                  fileCommandsFactory, databaseCommandsFactory, commandsHistory, fullPath)
        {
            _generator = generator;
            _treeViewFunctionality = treeViewFunctionality;
            _isMove = false;
        }

        public async Task ProcessNodeRecursive(INode sourceChild, INode targetParent, bool isMove)
        {
            _isMove = isMove;
            INode? existingChild = _treeViewFunctionality.GetChildrenByName(targetParent, sourceChild.Name);

            if (existingChild == null)
            {
                if (_isMove)
                {
                    await MoveItemOperation(sourceChild, targetParent, sourceChild.Name);
                }
                else
                {
                    await CopyItemOperation(sourceChild, targetParent, sourceChild.Name);
                }
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
                    await ProcessNodeRecursive(nestedChild, existingChild, _isMove);
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
                if (_isMove)
                {
                    await MoveItemOperation(sourceChild, targetParent, newName);
                }
                else
                {
                    await CopyItemOperation(sourceChild, targetParent, newName);
                }
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
                await DeleteItemOperation(node, LogCategory.DeleteFolderCategory);
            }
        }
    }
}
