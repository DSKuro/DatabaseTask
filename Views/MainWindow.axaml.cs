using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Services._serviceCollection;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.TreeView.EventArguments;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Transactions;

namespace DatabaseTask.Views
{
    public partial class MainWindow : Window
    {
        private readonly ITreeViewItemManager _treeViewManager;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(ITreeViewItemManager treeViewItemManager,
            IMessageBoxService messageBoxService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _treeViewManager = treeViewItemManager;
            _serviceProvider = serviceProvider;
            InitializeTree();
            InitializeMessages();
        }

        private void InitializeTree()
        {
            _treeViewManager.TreeViewItemInteractionData.Control = TreeViewControl;
            _treeViewManager.TreeViewItemInteractionData.Window = this;
            InitializeTreeEvents();
        }

        private void InitializeTreeEvents()
        {
            TreeViewControl.Loaded += (object? sender, RoutedEventArgs e) =>
            {
                _treeViewManager.TreeViewItemInteractionData.ScrollViewer = TreeViewControl.FindDescendantOfType<ScrollViewer>();
            };

            TreeViewControl.ContainerPrepared += (object? sender, ContainerPreparedEventArgs e) =>
            {
                _treeViewManager.ContainerPreparedEvent?.Invoke(this, e);
            };

            TreeViewControl.SelectionChanged += (object? sender, SelectionChangedEventArgs e) =>
            {
                ((MainWindowViewModel)DataContext).FileManager.TreeView.SelectionChanged?.Invoke(this, e);
            };
        }

        private void OnScrollChanged(object? sender, TreeViewEventArgs e)
        {
            _treeViewManager.TreeNodeOperations.BringIntoView(e.Node);
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
                    FolderOperationWindow createFolderWindow = CreateFolderDialogueWindow();
                    createFolderWindow.Title = WindowCategory.CreateFolderCategory.Value;
                    createFolderWindow.Watermark = TextBoxWatermark.CreateFolderWatermark.Value;
                    message.Reply(createFolderWindow.ShowDialog<string>(window));
                });

            WeakReferenceMessenger.Default.Register<MainWindow, MainWindowRenameFolderMessage>(this,
                (window, message) =>
                {
                    FolderOperationWindow createFolderWindow = CreateFolderDialogueWindow();
                    createFolderWindow.Title = WindowCategory.RenameFolderCategory.Value;
                    createFolderWindow.Watermark = TextBoxWatermark.RenameFolderWatermark.Value;
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

        private FolderOperationWindow CreateFolderDialogueWindow()
        {
            FolderOperationWindow window = _serviceProvider.GetRequiredService<FolderOperationWindow>();
            window.DataContext = _serviceProvider.GetRequiredService<FolderOperationWindowViewModel>();
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            return window;
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            ((MainWindowViewModel)DataContext).FileManager.TreeView.ScrollChanged += OnScrollChanged;
        }
    }
}
