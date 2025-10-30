using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Functionality;
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

            var comparer = new AdvancedExplorerComparer();
            NodeViewModel node = newNode as NodeViewModel;
            // Получаем свойства старого узла
            FileProperties? properties = _dataGrid.SavedFilesProperties.Find(x => x.Node == oldNode);
            if (properties == null) return;

            // Создаём новые свойства
            FileProperties newProperties = GetNewProperties(properties, newNode);

            // Ищем все свойства того же типа (файл/папка)
            var sameTypeProps = _dataGrid.SavedFilesProperties
                .Where(x => x.Node != null && (x.Node as NodeViewModel).IsFolder == node.IsFolder && x.Node.Parent == target)
                .ToList();

            // Находим индекс вставки
            int insertIndex = sameTypeProps.FindIndex(x => comparer.Compare(newNode.Name, x.Node.Name) < 0);

            int finalIndex;
            if (insertIndex == -1)
            {
                // Вставка в конец среди этого типа
                var last = sameTypeProps.LastOrDefault();
                finalIndex = last != null ? _dataGrid.SavedFilesProperties.IndexOf(last) + 1 : 0;
            }
            else
            {
                // Вставка перед найденным элементом
                var targetProp = sameTypeProps[insertIndex];
                finalIndex = _dataGrid.SavedFilesProperties.IndexOf(targetProp);
            }

            // Вставляем новые свойства
            _dataGrid.SavedFilesProperties.Insert(finalIndex, newProperties);
        }

        private FileProperties GetNewProperties(FileProperties oldProperties, INode node)
        {
            return new FileProperties(node.Name, oldProperties.Size, oldProperties.Modificated,
                oldProperties.IconPath, node);
        }
    }

}
