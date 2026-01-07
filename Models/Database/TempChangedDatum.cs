using System;
using System.Collections.Generic;

namespace DatabaseTask.Models.Database
{
    public partial class TempChangedDatum
    {
        public Guid? SessionId { get; set; }
        public int? ValueId { get; set; }
        public object? ValueData { get; set; }
    }
}
