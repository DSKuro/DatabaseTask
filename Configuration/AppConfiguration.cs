using DatabaseTask.Services.Commands;
using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.Dialogues.Base;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using DatabaseTask.Services.FileManagerOperations.Accessibility;
using DatabaseTask.Services.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.DataGrid;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using DatabaseTask.ViewModels.FileManager;
using DatabaseTask.ViewModels.FileManager.Interfaces;
using DatabaseTask.ViewModels.Interfaces;
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
            AddCommands();
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
            _serviceCollection.AddTransient<INode, NodeViewModel>();
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

        private void AddCommands()
        {
            _serviceCollection.AddScoped<ICreateFolderOperation, CreateFolderOperation>();
            _serviceCollection.AddScoped<IRenameFolderOperation, RenameFolderOperation>();
            _serviceCollection.AddScoped<IDeleteItemOperation, DeleteItemOperation>();
            _serviceCollection.AddScoped<IItemCommandsFactory, ItemCommandsFactory>();
        }

        private void AddViewModelsAndWindows()
        {
            _serviceCollection.AddScoped<IFileManagerFolderCommandsViewModel, FileManagerFolderCommandsViewModel>();
            _serviceCollection.AddScoped<IOpenDataViewModel, OpenDataViewModel>();
            _serviceCollection.AddTransient<FolderOperationWindowViewModel>();
            _serviceCollection.AddTransient<MainWindowViewModel>();
            _serviceCollection.AddTransient<FolderOperationWindow>();
        }
    }
}
