using System;

namespace DatabaseTask.Services.DataGrid.DataGridFunctionality.SubFunctionality.Interfaces
{
    public interface IDataGridFormatterService
    {
        public string TimeToString(DateTimeOffset? dateTimeOffset);

        public string SizeToString(ulong? size);
    }
}
