using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;
using System.Collections.Generic;

namespace DatabaseTask.Services.DataGrid.DataGridFunctionality
{
    public class DataGridFunctionality : IDataGridFunctionality
    {
        private IDataGridFormatterService _formatterService;
        private IDataGridPropertiesFunctionality _propertiesService;
        private IDataGridUpdatePropertiesService _updatePropertiesService;

        public DataGridFunctionality(
            IDataGridFormatterService formatterService,
            IDataGridPropertiesFunctionality propertiesService,
            IDataGridUpdatePropertiesService updatePropertiesService
           )
        {
            _formatterService = formatterService;
            _propertiesService = propertiesService;
            _updatePropertiesService = updatePropertiesService;
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

        public void ClearSavedProperties() => _updatePropertiesService.ClearSavedProperties();

        public void ClearFilesProperties() => _updatePropertiesService.ClearFilesProperties();

        public (IEnumerable<FileProperties> folders, IEnumerable<FileProperties> files) GetChildFoldersAndFilesProperties(INode selectedNode)
            => _updatePropertiesService.GetChildFoldersAndFilesProperties(selectedNode);
    }
}
