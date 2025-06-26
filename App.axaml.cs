using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using DatabaseTask.Services.Dialogues.Base;
using DatabaseTask.Services.Dialogues.FilePicker;
using DatabaseTask.ViewModels;
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
            collection.AddTransient<DialogueManager>();
            collection.AddTransient<DialogueHelper>();
            collection.AddTransient<MainWindowViewModel>();
            collection.AddTransient<IFilePickerService, FilePickerService>();
            return collection;
        }
    }
}