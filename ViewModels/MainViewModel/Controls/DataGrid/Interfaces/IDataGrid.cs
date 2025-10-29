using DatabaseTask.Models;
using System;
using System.Collections.Generic;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces
{
    public interface IDataGrid
    {
        public List<FileProperties> SavedFilesProperties { get; set; }
        public SmartCollection<FileProperties> FilesProperties { get; }
    }
}
