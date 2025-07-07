using DatabaseTask.Models;
using DatabaseTask.Services._serviceCollection;
using DatabaseTask.ViewModels.DataGrid.Interfaces;
using System;
using System.Collections.Generic;

namespace DatabaseTask.ViewModels.DataGrid
{
    public class DataGridService : IDataGrid
    {
        public List<FileProperties> SavedFilesProperties { get; set; } = new List<FileProperties>();
        public Smart_serviceCollection<FileProperties> FilesProperties { get; } = new Smart_serviceCollection<FileProperties>();

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
    }
}
