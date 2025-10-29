using DatabaseTask.Models;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid
{
    public class DataGridService : IDataGrid
    {
        public List<FileProperties> SavedFilesProperties { get; set; } = new List<FileProperties>();
        public SmartCollection<FileProperties> FilesProperties { get; } = new SmartCollection<FileProperties>();
    }
}
