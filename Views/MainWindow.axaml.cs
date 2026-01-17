using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using DatabaseTask.Services.TreeViewLogic.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.Analyses.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using DatabaseTask.Views.Analyse;
using DatabaseTask.Views.Comparators.Enum;
using DatabaseTask.Views.Comparators.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DatabaseTask.Views
{
    public partial class MainWindow : Window
    {
        private readonly ITreeViewInitializer _treeViewInitializer;
        private readonly ITreeView _treeView;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(ITreeViewInitializer treeViewInitializer,
            ITreeView treeView,
            IMessageBoxService messageBoxService,
            IFileComparerFactory factory,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _treeViewInitializer = treeViewInitializer;
            _treeView = treeView;
            _serviceProvider = serviceProvider;
            _treeViewInitializer.Initialize(TreeViewControl, this);
            InitializeMessages();
            InitializeSortingComparers(factory);
        }

        private void InitializeMessages()
        {
            WeakReferenceMessenger.Default.Register<MainWindow, MainWindowToggleManagerButtons>(this,
            (window, message) =>
            {
                EnableManagerButtons(message.Value);
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
                createFolderWindow.Text = _treeView.SelectedNodes[0].Name;
                message.Reply(createFolderWindow.ShowDialog<string>(window));
            });

            WeakReferenceMessenger.Default.Register<MainWindow, MainWindowUnusedFilesMessage>(this,
            (window, message) =>
            {
                UnusedFilesWindow unusedFilesWindow = _serviceProvider.GetRequiredService<UnusedFilesWindow>();
                unusedFilesWindow.DataContext = _serviceProvider.GetRequiredService<IUnusedFilesViewModel>();
                unusedFilesWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                message.Reply(unusedFilesWindow.ShowDialog<List<string>>(window));
            });

            WeakReferenceMessenger.Default.Register<MainWindow, MainWindowDuplicatesFilesMessage>(this,
            (window, message) =>
            {
                DuplicatesFilesWindow duplicatesFilesWindow = _serviceProvider.GetRequiredService<DuplicatesFilesWindow>();
                duplicatesFilesWindow.DataContext = _serviceProvider.GetRequiredService<IDuplicatesFilesViewModel>();
                duplicatesFilesWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                message.Reply(duplicatesFilesWindow.ShowDialog<List<string>>(window));
            });
        }

        private void EnableManagerButtons(bool value)
        {
            BtnCreateFolder.IsEnabled = value;
            BtnRenameFolder.IsEnabled = value;
            BtnDeleteFolder.IsEnabled = value;
            BtnCopyFolder.IsEnabled = value;
            BtnMoveFile.IsEnabled = value;
            BtnCopyFile.IsEnabled = value;
            BtnDeleteFile.IsEnabled = value;
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

        private void InitializeSortingComparers(IFileComparerFactory factory)
        {
            dataGrid.Columns[0].CustomSortComparer = factory.CreateFileComparer(FileComparerType.FileNameComparer);
            dataGrid.Columns[1].CustomSortComparer = factory.CreateFileComparer(FileComparerType.FileSizeComparer);
            dataGrid.Columns[2].CustomSortComparer = factory.CreateFileComparer(FileComparerType.FileTimeComparer);
        }
    }
}
