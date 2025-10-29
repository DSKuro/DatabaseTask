using DatabaseTask.Services.Commands;
using DatabaseTask.Services.Commands.FilesCommands;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.ItemCommands;
using DatabaseTask.Services.Dialogues.Base;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using DatabaseTask.Services.TreeViewItemLogic;
using DatabaseTask.Services.TreeViewItemLogic.InteractionData;
using DatabaseTask.Services.TreeViewItemLogic.InteractionData.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.ControlsHelpers.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.UI;
using DatabaseTask.Services.TreeViewItemLogic.UI.Interfaces;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.Logger;
using DatabaseTask.ViewModels.Logger.Interfaces;
using DatabaseTask.ViewModels.MainViewModel;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager;
using DatabaseTask.ViewModels.MainViewModel.Controls.FileManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DatabaseTask.Services.TreeViewItemLogic.Operations;
using DatabaseTask.Services.TreeViewItemLogic.Operations.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.TreeDragDrop.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.TreeDragDrop;
using DatabaseTask.Services.TreeViewItemLogic.Interactions.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.Interactions;
using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.Base;
using DatabaseTask.Services.Commands.FilesCommands.Interfaces;
using DatabaseTask.Services.Operations.LoggerOperations;
using DatabaseTask.Services.Operations.FilesOperations;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations;
using DatabaseTask.Services.Operations.LoggerOperations.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Decorator;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.Operations.Utils.Interfaces;
using DatabaseTask.Services.Operations.Utils;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.Utils.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.FolderViewModels;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels;
using DatabaseTask.ViewModels.MainViewModel.MainSubViewModels.CommandsViewModels.CommonViewModels.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality;

namespace DatabaseTask.Configuration
{
    public class AppConfiguration
    {
        private ServiceCollection _serviceCollection;

        public AppConfiguration()
        {
            _serviceCollection = new ServiceCollection();
        }

        public ServiceProvider BuildServiceProvider()
        {
            BuildServiceCollectionImpl();
            return _serviceCollection.BuildServiceProvider();
        }

        private void BuildServiceCollectionImpl()
        {
            AddStorage();
            AddFileManager();
            AddVisualTreeView();
            AddLogger();
            AddCommands();
            AddFilesCommands();
            AddViewModelsAndWindows();
        }

        private void AddStorage()
        {
            _serviceCollection.AddTransient<IDialogueManager, DialogueManager>();
            _serviceCollection.AddTransient<IDialogueHelper, DialogueHelper>();
            _serviceCollection.AddTransient<IMessageBoxService, MessageBoxService>();
            _serviceCollection.AddTransient<IStorageService, StorageService>();
        }

        private void AddFileManager()
        {
            _serviceCollection.AddScoped<INode, NodeViewModel>();
            _serviceCollection.AddScoped<ITreeView, TreeViewService>();
            _serviceCollection.AddScoped<IDataGrid, DataGridService>();
            _serviceCollection.AddScoped<IDataGridFunctionality, DataGridFunctionality>();
            _serviceCollection.AddScoped<IFileManager, FileManager>();
            _serviceCollection.AddScoped<IFileManagerFolderOperationsPermissions, FileManagerFolderOperationsPermissions>();
            _serviceCollection.AddScoped<IFileManagerFileOperationsPermissions, FileManagerFileOperationsPermissions>();
        }

        private void AddVisualTreeView()
        {
            _serviceCollection.AddScoped<ITreeViewData, TreeViewItemInteractionData>();
            _serviceCollection.AddScoped<ITreeViewControlsHelper, TreeViewControlsHelper>();
            _serviceCollection.AddScoped<ITreeNodeOperations, TreeNodeOperations>();
            _serviceCollection.AddScoped<ITreeViewVisualOperations, TreeViewVisualOperations>();
            _serviceCollection.AddScoped<ITreeViewItemInteractions, TreeViewItemInteractions>();
            _serviceCollection.AddScoped<ITreeViewItemDragDrop, TreeViewItemDragDrop>();
            _serviceCollection.AddScoped<ITreeViewItemManager, TreeViewItemManager>();
            _serviceCollection.AddScoped<ITreeViewInitializer, TreeViewInitializer>();
            _serviceCollection.AddScoped<ITreeViewFunctionality, TreeViewFunctionality>();
        }

        private void AddLogger()
        {
            _serviceCollection.AddScoped<ILogger, Logger>();
            _serviceCollection.AddScoped<ILoggerOperations, LoggerOperations>();
        }

        private void AddCommands()
        {
            _serviceCollection.AddScoped<ICreateFolderOperation, CreateFolderOperation>();
            _serviceCollection.AddScoped<IRenameFolderOperation, RenameFolderOperation>();
            _serviceCollection.AddScoped<IDeleteItemOperation, DeleteItemOperation>();
            _serviceCollection.AddScoped<ICopyItemOperation, CopyItemOperation>();
            _serviceCollection.AddScoped<INameGenerator, NameGenerator>();
            _serviceCollection.AddScoped<MoveOperationDecorator>();
            _serviceCollection.AddTransient<ICompositeCommandBuilder, CompositeCommandBuilder>();
            _serviceCollection.AddScoped<ICommandsFactory, ItemCommandsFactory>();
        }

        private void AddFilesCommands()
        {
            _serviceCollection.AddSingleton<IFullPath, FullPath>();
            _serviceCollection.AddScoped<IFilesOperations, FilesOperations>();
            _serviceCollection.AddScoped<IFileCommandsFactory, FilesCommandsFactory>();
            _serviceCollection.AddSingleton<ICommandsHistory, CommandsHistory>();
        }

        private void AddViewModelsAndWindows()
        {
            _serviceCollection.AddScoped<IOpenDataViewModel, OpenDataViewModel>();
            _serviceCollection.AddScoped<ICreateFolderCommandsViewModel, CreateFolderCommandsViewModel>();
            _serviceCollection.AddScoped<IRenameFolderCommandsViewModel, RenameFolderCommandsViewModel>();
            _serviceCollection.AddScoped<IDeleteItemCommandsViewModel, DeleteItemCommandsViewModel>();
            _serviceCollection.AddScoped<IMoveFileCommandsViewModel, MoveFileCommandsViewModel>();
            _serviceCollection.AddScoped<ICopyFolderCommandsViewModel, CopyFolderCommandViewModel>();
            _serviceCollection.AddScoped<IMergeCommandsViewModel, MergeCommandsViewModel>();
            _serviceCollection.AddTransient<FolderOperationWindowViewModel>();
            _serviceCollection.AddTransient<MainWindowViewModel>();
            _serviceCollection.AddTransient<FolderOperationWindow>();
        }
    }
}
