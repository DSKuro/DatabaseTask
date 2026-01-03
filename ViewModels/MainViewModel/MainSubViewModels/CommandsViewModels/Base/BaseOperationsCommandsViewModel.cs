using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.Utility.Enum;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Base
{
    public class BaseOperationsCommandsViewModel : BaseFileManagerCommandsViewModel, IBaseOperationsCommandsViewModel
    {
        public BaseOperationsCommandsViewModel(IMessageBoxService messageBoxService,
            ICommandsFactory itemCommandsFactory, IFileCommandsFactory fileCommandsFactory,
            ICommandsHistory commandsHistory, IFullPath fullPath) 
            : base(messageBoxService, itemCommandsFactory, fileCommandsFactory, commandsHistory, fullPath)
        {
        }

        public async Task CreateFolderOperation(string name)
        {
            await ProcessCommand(new Models.DTO.CommandInfo
                (
                    CommandType.CreateFolder, name
                ),
                new Models.DTO.LoggerDTO
                (
                    LogCategory.CreateFolderCategory,
                    name.ToString()!
                )
            );
        }

        public async Task DeleteItemOperation(INode node, LogCategory category)
        {
            await ProcessCommand(new Models.DTO.CommandInfo
                (
                    CommandType.DeleteItem, node
                ),
                new Models.DTO.LoggerDTO
                (
                    category,
                    node.Name
                )
            );
        }

        public async Task CopyItemOperation(INode node, INode target, string name)
        {
            bool isFolder = false;
            if (node is NodeViewModel newNode)
            {
                isFolder = newNode.IsFolder;
            }

            await ProcessCommand(new Models.DTO.CommandInfo
               (
                   CommandType.CopyItem, node,
                   target,
                   name
               ),
               new Models.DTO.LoggerDTO
               (
                   isFolder ? LogCategory.CopyFolderCategory : LogCategory.CopyFileCategory,
                   name ?? node.Name,
                   target.Name
               )
            );
        }

        public async Task MoveItemOperation(INode node, INode target, string name)
        {
            bool isFolder = false;
            if (node is NodeViewModel newNode) {
                isFolder = newNode.IsFolder;
            }

            await ProcessCommand(new Models.DTO.CommandInfo
               (
                   CommandType.MoveFile, node,
                   target,
                   name
               ),
               new Models.DTO.LoggerDTO
               (
                   (isFolder) ? LogCategory.MoveCatalogCategory
                   : LogCategory.MoveFileCategory,
                   name ?? node.Name,
                   target.Name
               )
            );
        }

        public async Task RenameFolderOperation(string oldName, string newName)
        {
            await ProcessCommand(new Models.DTO.CommandInfo
                (
                    CommandType.RenameFolder, oldName, newName
                ),
                new Models.DTO.LoggerDTO
                (
                    LogCategory.RenameFolderCategory,
                    oldName,
                    newName
                )
            );
        }
    }
}
