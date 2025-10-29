using DatabaseTask.Models.Categories;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;
using System.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality
{
    public class DataGridFunctionality : IDataGridFunctionality
    {
        private IDataGrid _dataGrid;

        public DataGridFunctionality(IDataGrid dataGrid)
        {
            _dataGrid = dataGrid;
        }

        public string TimeToString(DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset?.ToString("HH:mm") ?? "";
        }

        public string SizeToString(ulong? size)
        {
            if (size == null)
            {
                return "";
            }
            return $"{Math.Ceiling((double)size / 1024)} KB";
        }

        public bool TryInsertProperties(int parentIndex, INode parent, FileProperties properties)
        {
            int index = _dataGrid.SavedFilesProperties.FindIndex(x => x.Node == parent);
            if (index >= 0)
            {
                _dataGrid.SavedFilesProperties.Insert(parentIndex + index, properties);
                return true;
            }
            return false;
        }

        public FileProperties? GetPropertiesForNode(INode node)
        {
            return _dataGrid.SavedFilesProperties.FirstOrDefault(x => x.Node == node);
        }
    }
}
