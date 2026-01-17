using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels
{
    public class DatabaseInteractionViewModel : BaseOperationsCommandsViewModel, IDatabaseInteractionViewModel
    {
        private readonly IValidateViewModel _validateViewModel;
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public DatabaseInteractionViewModel(
            IMessageBoxService messageBoxService,
            ILoggerCommandsFactory itemCommandsFactory, 
            IFileCommandsFactory fileCommandsFactory,
            IDatabaseCommandsFactory databaseCommandsFactory,
            ICommandsHistory commandsHistory, 
            IFullPath fullPath,
            IValidateViewModel validateViewModel,
            ITreeViewFunctionality treeViewFunctionality)
            : base(messageBoxService, itemCommandsFactory, fileCommandsFactory,
                  databaseCommandsFactory, commandsHistory, fullPath,
                  treeViewFunctionality)
        {
            _validateViewModel = validateViewModel;
            _treeViewFunctionality = treeViewFunctionality;
        }

        public async Task FindDuplicates()
        {
            if (!await _validateViewModel.ValidateCatalogAndDatabaseAsync())
            {
                return;
            }

            var results = await WeakReferenceMessenger.Default.Send<MainWindowDuplicatesFilesMessage>();

            if (results is not null)
            {
                await DeleteFiles(results);
            }
        }

        public async Task FindUnusedFiles()
        {
            if (!await _validateViewModel.ValidateCatalogAndDatabaseAsync())
            {
                return;
            }

            var paths = await WeakReferenceMessenger.Default.Send<MainWindowUnusedFilesMessage>();

            if (paths is not null)
            {
                await DeleteFiles(paths);
            }
        }

        private async Task DeleteFiles(List<string> paths)
        {
            foreach (var path in paths)
            {
                INode? node = _treeViewFunctionality.GetNodeByPath(path);
                if (node is not null)
                {
                    await DeleteItemOperation(node, LogCategory.DeleteFileCategory);
                }
            }
        }
    }
}
