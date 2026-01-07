using System;
using System.Collections.Generic;

namespace DatabaseTask.Models.Database
{
    public partial class TempDataTable
    {
        public Guid? SessionId { get; set; }
        public int? ValueTable { get; set; }
        public int? ValueField { get; set; }
        public object? ValueData { get; set; }
        public int? TempRowId { get; set; }
    }
}
