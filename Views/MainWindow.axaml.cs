using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using DatabaseTask.Services.TreeView;
using DatabaseTask.ViewModels;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace DatabaseTask.Views
{
    public partial class MainWindow : Window
    {
        private ITreeViewItem _treeViewItemLogic;

        public MainWindow(ITreeViewItem treeViewItemLogic)
        {
            InitializeComponent();
           _treeViewItemLogic = treeViewItemLogic;
            _treeViewItemLogic._mainWindow = this;
            _treeViewItemLogic._treeViewControl = TreeViewControl;
            _treeViewItemLogic._scrollViewer = TreeViewControl.FindDescendantOfType<ScrollViewer>();
            TreeViewControl.ContainerPrepared += _treeViewItemLogic.OnContainerPrepared;
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
            _treeViewItemLogic.MainWindowViewModel = DataContext as MainWindowViewModel;
        }
    }
}
