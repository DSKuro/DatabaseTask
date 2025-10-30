using DatabaseTask.Services.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.DataGrid.DataGridFunctionality.SubFunctionality
{
    public class DataGridUpdatePropertiesService : IDataGridUpdatePropertiesService
    {
        private IDataGrid _dataGrid;

        public DataGridUpdatePropertiesService(IDataGrid dataGrid)
        {
            _dataGrid = dataGrid;
        }

        public void ClearSavedProperties() => _dataGrid.SavedFilesProperties.Clear();
        public void ClearFilesProperties() => _dataGrid.FilesProperties.Clear();

        public (IEnumerable<FileProperties>, IEnumerable<FileProperties>) GetChildFoldersAndFilesProperties(INode selectedNode)
        {
            IEnumerable<FileProperties> selectedNodeChilds = _dataGrid.SavedFilesProperties
                .Where(x => x.Node.Parent == selectedNode)
                .Where(x => x.Node is NodeViewModel);
            IEnumerable<FileProperties> folders = selectedNodeChilds
                .Where(x => ((NodeViewModel)x.Node).IsFolder);
            IEnumerable<FileProperties> files = selectedNodeChilds
                .Where(x => !((NodeViewModel)x.Node).IsFolder);
            return (folders, files);
        }
    }
}
