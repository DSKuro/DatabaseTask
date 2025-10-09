using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseTask.Views
{
    public partial class MainWindow : Window
    {
        private readonly ITreeViewInitializer _treeViewInitializer;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(ITreeViewInitializer treeViewInitializer,
            IMessageBoxService messageBoxService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _treeViewInitializer = treeViewInitializer;
            _serviceProvider = serviceProvider;
            _treeViewInitializer.Initialize(TreeViewControl, this);
            InitializeMessages();
        }

        private void InitializeMessages()
        {
            WeakReferenceMessenger.Default.Register<MainWindow, MainWindowEnableManagerButtons>(this,
            (window, message) =>
            {
                if (!BtnCreateFolder.IsEnabled)
                {
                    EnableManagerButtons();
                }
            });

            WeakReferenceMessenger.Default.Register<MainWindow, MainWindowCreateFolderMessage>(this,
            (window, message) =>
            {
                FolderOperationWindow createFolderWindow =
                ProcessFolderWindowMessage(WindowCategory.CreateFolderCategory.Value,
                    TextBoxWatermark.CreateFolderWatermark.Value);
                message.Reply(createFolderWindow.ShowDialog<string>(window));
            });

            WeakReferenceMessenger.Default.Register<MainWindow, MainWindowRenameFolderMessage>(this,
            (window, message) =>
            {
                FolderOperationWindow createFolderWindow = 
                ProcessFolderWindowMessage(WindowCategory.RenameFolderCategory.Value,
                    TextBoxWatermark.RenameFolderWatermark.Value);
                message.Reply(createFolderWindow.ShowDialog<string>(window));
            });
        }

        private void EnableManagerButtons()
        {
            BtnCreateFolder.IsEnabled = true;
            BtnRenameFolder.IsEnabled = true;
            BtnDeleteFolder.IsEnabled = true;
            BtnCopyFolder.IsEnabled = true;
            BtnMoveFile.IsEnabled = true;
            BtnCopyFile.IsEnabled = true;
            BtnDeleteFile.IsEnabled = true;
        }

        private FolderOperationWindow ProcessFolderWindowMessage(string title, string watermark)
        {
            FolderOperationWindow createFolderWindow = CreateFolderDialogueWindow();
            createFolderWindow.Title = title;
            createFolderWindow.Watermark = watermark;
            return createFolderWindow;
        }

        private FolderOperationWindow CreateFolderDialogueWindow()
        {
            FolderOperationWindow window = _serviceProvider.GetRequiredService<FolderOperationWindow>();
            window.DataContext = _serviceProvider.GetRequiredService<FolderOperationWindowViewModel>();
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            return window;
        }
    }
}
