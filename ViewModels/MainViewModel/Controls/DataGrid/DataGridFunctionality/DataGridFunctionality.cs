using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality
{
    public class DataGridFunctionality : IDataGridFunctionality
    {
        private IDataGridFormatterService _formatterService;
        private IDataGridPropertiesFunctionality _propertiesService;

        public DataGridFunctionality(
            IDataGridFormatterService formatterService,
            IDataGridPropertiesFunctionality propertiesService
           )
        {
            _formatterService = formatterService;
            _propertiesService = propertiesService;
        }

        public string SizeToString(ulong? size) => _formatterService.SizeToString(size);

        public string TimeToString(DateTimeOffset? dateTimeOffset) => _formatterService.TimeToString(dateTimeOffset);

        public bool TryInsertProperties(int parentIndex, INode parent, FileProperties properties) 
            => _propertiesService.TryInsertProperties(parentIndex, parent, properties);
        public FileProperties? GetPropertiesForNode(INode node) => _propertiesService.GetPropertiesForNode(node);
        public void AddProperties(FileProperties properties) => _propertiesService.AddProperties(properties);
        public void CopyProperties(INode oldNode, INode newNode, INode target) 
            => _propertiesService.CopyProperties(oldNode, newNode, target);
        public void RemoveProperties(INode node) => _propertiesService?.RemoveProperties(node);
    }
}
