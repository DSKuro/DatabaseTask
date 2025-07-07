using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
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
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using DatabaseTask.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseTask
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            BindingPlugins.DataValidators.RemoveAt(0);

            ServiceCollection collection = BuildCollection();
            ServiceProvider services = collection.BuildServiceProvider();

            MainWindowViewModel viewModel = services.GetRequiredService<MainWindowViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow(services.GetRequiredService<ITreeViewItemManager>(),
                    services.GetRequiredService<IMessageBoxService>(), services)
                {
              
                    DataContext = viewModel,
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainWindow(services.GetRequiredService<ITreeViewItemManager>(),
                    services.GetRequiredService<IMessageBoxService>(),
                    services)
                {
                    DataContext = viewModel
                };
            }

                base.OnFrameworkInitializationCompleted();
        }

        private ServiceCollection BuildCollection()
        {
            ServiceCollection collection = new ServiceCollection();
            AddStorage(collection);
            AddFileManager(collection);
            AddVisualTreeView(collection);
            AddCommands(collection);
            AddViewModelsAndWindows(collection);
            return collection;
        }

        private void AddStorage(ServiceCollection collection)
        {
            collection.AddTransient<IDialogueManager, DialogueManager>();
            collection.AddTransient<IDialogueHelper, DialogueHelper>();
            collection.AddTransient<IMessageBoxService, MessageBoxService>();
            collection.AddTransient<IStorageService, StorageService>();
        }

        private void AddFileManager(ServiceCollection collection)
        {
            collection.AddTransient<INode, NodeViewModel>();
            collection.AddScoped<ITreeView, TreeViewService>();
            collection.AddScoped<IDataGrid, DataGridService>();
            collection.AddScoped<IFileManager, FileManager>();
            collection.AddScoped<IFileManagerOperationsPermissions, FileManagerOperationsPermissions>();
        }

        private void AddVisualTreeView(ServiceCollection collection)
        {
            collection.AddScoped<ITreeViewData, TreeViewItemInteractionData>();
            collection.AddScoped<ITreeViewControlsHelper, TreeViewControlsHelper>();
            collection.AddScoped<ITreeNodeOperations, TreeNodeOperations>();
            collection.AddScoped<ITreeViewVisualOperations, TreeViewVisualOperations>();
            collection.AddScoped<ITreeViewItemInteractions, TreeViewItemInteractions>();
            collection.AddScoped<ITreeViewItemDragDrop, TreeViewItemDragDrop>();
            collection.AddTransient<ITreeViewItemManager, TreeViewItemManager>();
        }

        private void AddCommands(ServiceCollection collection)
        {
            collection.AddScoped<ICreateFolderOperation, CreateFolderOperation>();
            collection.AddScoped<IRenameFolderOperation, RenameFolderOperation>();
            collection.AddScoped<IFolderCommandsFactory, FolderCommandsFactory>();
        }

        private void AddViewModelsAndWindows(ServiceCollection collection)
        {
            collection.AddTransient<FolderOperationWindowViewModel>();
            collection.AddScoped<MainWindowViewModel>();
            collection.AddTransient<FolderOperationWindow>();
        }
    }
}