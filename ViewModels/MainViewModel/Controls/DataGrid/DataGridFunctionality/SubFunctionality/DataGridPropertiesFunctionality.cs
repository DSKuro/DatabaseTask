using DatabaseTask.Services.Comparer.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Linq;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.SubFunctionality
{
    public class DataGridPropertiesFunctionality : IDataGridPropertiesFunctionality
    {
        private readonly IDataGrid _dataGrid;
        private readonly INodeComparer _nodeComparer;

        public DataGridPropertiesFunctionality(IDataGrid datagrid, INodeComparer nodeComparer)
        {
            _dataGrid = datagrid;
            _nodeComparer = nodeComparer;
        }

        public FileProperties? GetPropertiesForNode(INode node)
        {
            return _dataGrid.SavedFilesProperties.FirstOrDefault(x => x.Node == node);
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

            AddProperties(newProperties);
        }

        public void AddProperties(FileProperties properties)
        {
            int insertIndex = _dataGrid.SavedFilesProperties.FindIndex(x => _nodeComparer.Compare(properties.Node,
               x.Node) < 0);

            if (insertIndex < 0)
            {
                insertIndex = _dataGrid.SavedFilesProperties.Count;
            }

            _dataGrid.SavedFilesProperties.Insert(insertIndex, properties);
        }

        private FileProperties GetNewProperties(FileProperties oldProperties, INode node)
        {
            return new FileProperties(node.Name, oldProperties.Size, oldProperties.Modificated,
                oldProperties.IconPath, node);
        }
    }
}
