using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces
{
    public interface IDataGridUpdatePropertiesService
    {
        public void ClearSavedProperties();
        public void ClearFilesProperties();
        (IEnumerable<FileProperties> folders, IEnumerable<FileProperties> files) GetChildFoldersAndFilesProperties(INode selectedNode);
    }
}
