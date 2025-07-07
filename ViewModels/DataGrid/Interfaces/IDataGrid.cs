using DatabaseTask.Models;
using DatabaseTask.Services._serviceCollection;
using System;
using System.Collections.Generic;

namespace DatabaseTask.ViewModels.DataGrid.Interfaces
{
    public interface IDataGrid
    {
        public List<FileProperties> SavedFilesProperties { get; set; }
        public Smart_serviceCollection<FileProperties> FilesProperties { get; }

        public string TimeToString(DateTimeOffset? dateTimeOffset);
        public string SizeToString(ulong? size);
    }
}
