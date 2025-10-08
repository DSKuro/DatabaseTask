using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using DatabaseTask.Configuration;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels.MainViewModel;
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

            ServiceProvider services = new AppConfiguration().BuildServiceProvider();

            MainWindowViewModel viewModel = services.GetRequiredService<MainWindowViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow(services.GetRequiredService<ITreeViewInitializer>(),
                    services.GetRequiredService<IMessageBoxService>(), services)
                {
              
                    DataContext = viewModel,
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainWindow(services.GetRequiredService<ITreeViewInitializer>(),
                    services.GetRequiredService<IMessageBoxService>(),
                    services)
                {
                    DataContext = viewModel
                };
            }

                base.OnFrameworkInitializationCompleted();
        }
    }
}