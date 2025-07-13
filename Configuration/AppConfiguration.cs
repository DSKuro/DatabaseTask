using DatabaseTask.Services.Commands;
using DatabaseTask.Services.Commands.FilesCommands;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Commands.ItemCommands;
using DatabaseTask.Services.Commands.LogCommands;
using DatabaseTask.Services.Dialogues.Base;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using DatabaseTask.Services.FileManagerOperations.Accessibility;
using DatabaseTask.Services.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Decorator;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.FilesOperations;
using DatabaseTask.Services.FilesOperations.Interfaces;
using DatabaseTask.Services.LoggerOperations;
using DatabaseTask.Services.LoggerOperations.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.DataGrid;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.FileManager;
using DatabaseTask.ViewModels.FileManager.Interfaces;
using DatabaseTask.ViewModels.Interfaces;
using DatabaseTask.ViewModels.Logger;
using DatabaseTask.ViewModels.Logger.Interfaces;
using DatabaseTask.ViewModels.MainSubViewModels;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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
            _serviceCollection.AddTransient<ITreeViewItemManager, TreeViewItemManager>();
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
            _serviceCollection.AddTransient<IGetParamsForLog, GetParamsForLog>();
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
            _serviceCollection.AddScoped<IMessageBoxCommandsViewModel, MessageBoxCommandsViewModel>();
            _serviceCollection.AddScoped<IOpenDataViewModel, OpenDataViewModel>();
            _serviceCollection.AddScoped<IFolderCommandsViewModel, FolderCommandsViewModel>();
            _serviceCollection.AddTransient<FolderOperationWindowViewModel>();
            _serviceCollection.AddTransient<MainWindowViewModel>();
            _serviceCollection.AddTransient<FolderOperationWindow>();
        }
    }
}
