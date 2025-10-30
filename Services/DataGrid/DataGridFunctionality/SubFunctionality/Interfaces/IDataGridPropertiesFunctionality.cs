using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces
{
    public interface IDataGridPropertiesFunctionality
    {
        public FileProperties? GetPropertiesForNode(INode node);
        public bool TryInsertProperties(int parentIndex, INode parent, FileProperties properties);
        public void RemoveProperties(INode node);
        public void AddProperties(FileProperties properties);
        public void CopyProperties(INode oldNode, INode newNode, INode target);
    }
}
