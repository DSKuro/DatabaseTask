using DatabaseTask.Services.Comparer.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;
using System.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality
{
    public class DataGridFunctionality : IDataGridFunctionality
    {
        private IDataGrid _dataGrid;
        private INodeComparer _nodeComparer;

        public DataGridFunctionality(IDataGrid dataGrid,
            INodeComparer comparer)
        {
            _dataGrid = dataGrid;
            _nodeComparer = comparer;
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

        public void RemoveProperties(INode node)
        {
            FileProperties? properties = _dataGrid.SavedFilesProperties.Find(x => x.Node == node);
            if (properties != null)
            {
                _dataGrid.SavedFilesProperties.Remove(properties);
            }
        }

        public void CopyProperties(INode oldNode, INode newNode, INode target)
        {
            if (newNode == null) return;

            FileProperties? properties = _dataGrid.SavedFilesProperties.Find(x => x.Node == oldNode);
            if (properties == null) return;

            FileProperties newProperties = GetNewProperties(properties, newNode);

            int insertIndex = _dataGrid.SavedFilesProperties.FindIndex(x => _nodeComparer.Compare(newProperties.Node,
                target) < 0);

            _dataGrid.SavedFilesProperties.Insert(insertIndex, newProperties);
        }

        private FileProperties GetNewProperties(FileProperties oldProperties, INode node)
        {
            return new FileProperties(node.Name, oldProperties.Size, oldProperties.Modificated,
                oldProperties.IconPath, node);
        }
    }
}
