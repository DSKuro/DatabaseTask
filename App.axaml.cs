using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using DatabaseTask.Services.Dialogues.Base;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Dialogues.Storage;
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
                desktop.MainWindow = new MainWindow
                {
                    DataContext = viewModel,
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainWindow
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
            collection.AddTransient<MainWindowViewModel>();
            collection.AddTransient<IStorageService, StorageService>();
            collection.AddTransient<IMessageBoxService, MessageBoxService>();
            return collection;
        }
    }
}