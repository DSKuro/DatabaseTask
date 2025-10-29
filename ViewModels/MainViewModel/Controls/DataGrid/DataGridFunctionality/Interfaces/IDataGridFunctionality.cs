using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces
{
    public interface IDataGridFunctionality
    {
        public string TimeToString(DateTimeOffset? dateTimeOffset);

        public string SizeToString(ulong? size);
        public bool TryInsertProperties(int parentIndex, INode parent, FileProperties properties);
        public FileProperties? GetPropertiesForNode(INode node);
        public void RemoveProperties(INode node);
    }
}
