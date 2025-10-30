using System;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces
{
    public interface IDataGridFormatterService
    {
        public string TimeToString(DateTimeOffset? dateTimeOffset);

        public string SizeToString(ulong? size);
    }
}
