using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using DatabaseTask.Services.Dialogues.Base;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using DatabaseTask.Services.Exceptions;
using DatabaseTask.Services.Exceptions.Interfaces;
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
                desktop.MainWindow = new MainWindow(services.GetRequiredService<ITreeViewItemManager>())
                {
              
                    DataContext = viewModel,
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainWindow(services.GetRequiredService<ITreeViewItemManager>())
                {
                    DataContext = viewModel
                };
            }

                base.OnFrameworkInitializationCompleted();
        }

        private ServiceCollection BuildCollection()
        {
            ServiceCollection collection = new ServiceCollection();
            collection.AddTransient<IDialogueManager, DialogueManager>();
            collection.AddTransient<IDialogueHelper, DialogueHelper>();
            collection.AddScoped<IDataGrid, DataGridService>();
            collection.AddTransient<IFileManager, FileManager>();
            collection.AddTransient<INode, NodeViewModel>();
            collection.AddTransient<ITreeView, TreeViewService>();
            collection.AddScoped<ITreeViewData, TreeViewItemInteractionData>();
            collection.AddScoped<ITreeViewControlsHelper, TreeViewControlsHelper>();
            collection.AddScoped<ITreeNodeOperations, TreeNodeOperations>();
            collection.AddScoped<ITreeViewVisualOperations, TreeViewVisualOperations>();
            collection.AddScoped<ITreeViewItemInteractions, TreeViewItemInteractions>();
            collection.AddScoped<ITreeViewItemDragDrop, TreeViewItemDragDrop>();
            collection.AddScoped<IExceptionHandler, ExceptionHandler>();
            collection.AddTransient<ITreeViewItemManager, TreeViewItemManager>();
            collection.AddScoped<MainWindowViewModel>();
            collection.AddTransient<IStorageService, StorageService>();
            collection.AddTransient<IMessageBoxService, MessageBoxService>();
            return collection;
        }
    }
}