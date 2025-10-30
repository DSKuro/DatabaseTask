using Avalonia.Controls;
using DatabaseTask.Models.Categories;
using DatabaseTask.Services.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager
{
    public class TreeViewEventService : ITreeViewEventService
    {
        private readonly IDataGridUpdatePropertiesService _propertiesFunctionality;
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly ITreeView _treeView;
        private readonly IDataGrid _dataGrid;

        public TreeViewEventService(ITreeView treeView, IDataGrid dataGrid,
                                   IDataGridUpdatePropertiesService propertiesFunctionality,
                                   ITreeViewFunctionality treeViewFunctionality)
        {
            _treeView = treeView;
            _treeView.SelectionChanged += HandleSelectionChanged;
            _dataGrid = dataGrid;
            _propertiesFunctionality = propertiesFunctionality;
            _treeViewFunctionality = treeViewFunctionality;
        }

        public void SubscribeToNodeEvents(INode node)
        {
            node.Expanded += ExpandHandler;
            node.Collapsed += CollapsedHandler;
        }

        public void UnsubscribeFromNodeEvents(INode node)
        {
            node.Expanded -= ExpandHandler;
            node.Collapsed -= CollapsedHandler;
        }

        public void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            _dataGrid.FilesProperties.Clear();
            if (_treeViewFunctionality.GetAllSelectedNodes().Count == 1)
            {
                UpdatePropertiesOnSelection();
            }
        }

        public void ExpandHandler(INode model) => ExpandCollapsedImpl(model, IconCategory.OpenedFolder);
        public void CollapsedHandler(INode model) => ExpandCollapsedImpl(model, IconCategory.Folder);

        private void ExpandCollapsedImpl(INode model, IconCategory iconPath)
        {
            if (model is NodeViewModel node)
            {
                node.IconPath = iconPath.Value;
                _treeViewFunctionality.GetAllSelectedNodes().Clear();
                _treeViewFunctionality.GetAllSelectedNodes().Add(node);
            }
        }

        private void UpdatePropertiesOnSelection()
        {
            INode? node = _treeViewFunctionality.GetFirstSelectedNode();
            if (node != null)
            {
                var (folders, files) = _propertiesFunctionality.GetChildFoldersAndFilesProperties(node);
                _dataGrid.FilesProperties.AddRange(folders);
                _dataGrid.FilesProperties.AddRange(files);
            }
        }
    }
}
