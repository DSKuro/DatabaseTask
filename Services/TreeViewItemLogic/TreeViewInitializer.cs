using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using DatabaseTask.Services.TreeViewItemLogic.Interfaces;
using DatabaseTask.Services.TreeViewItemLogic.Operations.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;

namespace DatabaseTask.Services.TreeViewItemLogic
{
    public class TreeViewInitializer : ITreeViewInitializer
    {
        private readonly ITreeViewItemManager _manager;
        private readonly ITreeView _treeView;
        private readonly ITreeNodeOperations _treeNodeOperations;

        public TreeViewInitializer(
            ITreeViewItemManager manager,
            ITreeView treeView,
            ITreeNodeOperations treeNodeOperations)
        {
            _manager = manager;
            _treeView = treeView;
            _treeNodeOperations = treeNodeOperations;
        }

        public void Initialize(TreeView treeView, Window window)
        {
            _manager.Initialize(treeView, window);
            InitializeEvents(treeView);
            _treeView.ScrollChanged += OnScrollChanged;
        }

        private void InitializeEvents(TreeView treeView)
        {
            treeView.Loaded += (object? sender, RoutedEventArgs e) =>
            {
                ScrollViewer? scroll = treeView.FindDescendantOfType<ScrollViewer>(); 
                if (scroll != null)
                {
                    _manager.InitializeScrollViewer(scroll);
                }
            };

            treeView.ContainerPrepared += (object? sender, ContainerPreparedEventArgs e) =>
            {
                _manager.ContainerPreparedEvent?.Invoke(this, e);
            };

            treeView.SelectionChanged += (object? sender, SelectionChangedEventArgs e) =>
            {
                _treeView.SelectionChanged?.Invoke(this, e);
            };
        }

        private void OnScrollChanged(object? sender, TreeViewEventArgs e)
        {
            _treeNodeOperations.BringIntoView(e.Node);
        }
    }
}
