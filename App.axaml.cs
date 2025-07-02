using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using DatabaseTask.Services.Dialogues.Base;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
using DatabaseTask.Services.TreeViewItemLogic;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView;
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
            collection.AddTransient<IGetTreeNodes, GetTreeNodesService>();
            collection.AddTransient<INode, NodeViewModel>();
            collection.AddTransient<ITreeView, TreeViewService>();
            collection.AddScoped<ITreeViewData, TreeViewItemInteractionData>();
            collection.AddScoped<ITreeViewItemInteractions, TreeViewItemInteractions>();
            collection.AddScoped<ITreeViewItemDragDrop, TreeViewItemDragDrop>();
            collection.AddTransient<ITreeViewItemManager, TreeViewItemManager>();
            collection.AddScoped<MainWindowViewModel>();
            collection.AddTransient<IStorageService, StorageService>();
            collection.AddTransient<IMessageBoxService, MessageBoxService>();
            return collection;
        }
    }
}