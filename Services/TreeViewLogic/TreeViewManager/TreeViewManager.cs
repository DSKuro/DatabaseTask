using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.SubFunctionality.Interfaces;
using DatabaseTask.Services.TreeViewLogic.TreeViewManager.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.TreeViewLogic.TreeViewManager
{
    public class TreeViewManager : ITreeViewManager
    {
        private readonly ITreeView _treeView;
        private readonly IDataGridFunctionality _dataGridFunctionality;
        private readonly ITreeViewNodeService _nodeService;

        public TreeViewManager(ITreeView treeView,
            IDataGridFunctionality dataGridFunctionality,
            ITreeViewNodeService nodeService)
        {
            _treeView = treeView;
            _dataGridFunctionality = dataGridFunctionality;
            _nodeService = nodeService;
        }

        public async Task LoadFoldersAsync(IEnumerable<string> folderPaths)
        {
            _treeView.Nodes.Clear();
            _dataGridFunctionality.ClearSavedProperties();
            _dataGridFunctionality.ClearFilesProperties();

            var nodes = await _nodeService.CreateNodesFromPathsAsync(folderPaths);
            _treeView.Nodes.AddRange(nodes);
        }
    }
}
