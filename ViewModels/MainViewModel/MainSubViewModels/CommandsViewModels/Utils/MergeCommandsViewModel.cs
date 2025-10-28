using DatabaseTask.Models.Categories;
using DatabaseTask.Models.MessageBox;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.Operations.Utils.Interfaces;
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
        private INameGenerator _generator;

        public MergeCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory,
            IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory, IFullPath fullPath,
            INameGenerator generator)
            : base(messageBoxService, itemCommandsFactory, fileCommandsFactory, commandsHistory, fullPath)
        {
            _generator = generator;
        }

        public async Task ProcessNodeRecursive(INode sourceChild, INode targetParent)
        {
            INode? existingChild = targetParent.Children.FirstOrDefault(x => x.Name == sourceChild.Name);

            if (existingChild == null)
            {
                await MoveItemOperation(sourceChild, targetParent, sourceChild.Name);
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
                    await ProcessNodeRecursive(nestedChild, existingChild);
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
                sourceChild.Name = newName;
                await MoveItemOperation(sourceChild, targetParent, sourceChild.Name);
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
