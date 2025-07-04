using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.ViewModels;
using System;

namespace DatabaseTask.Views
{
    public partial class MainWindow : Window
    {
        private readonly ITreeViewItemManager _treeViewManager;

        public MainWindow(ITreeViewItemManager treeViewItemManager)
        {
            InitializeComponent();

            _treeViewManager = treeViewItemManager;
            InitializeTree();
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
                ((MainWindowViewModel)DataContext).GetTreeNodes.TreeView.SelectionChanged?.Invoke(this, e);
            };
        }
    }
}
