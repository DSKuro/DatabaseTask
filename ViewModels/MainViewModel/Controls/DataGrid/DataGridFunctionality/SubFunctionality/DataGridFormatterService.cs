using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces;
using System;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.SubFunctionality
{
    public class DataGridFormatterService : IDataGridFormatterService
    {
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
