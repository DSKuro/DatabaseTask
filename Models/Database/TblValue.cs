using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblValue
    {
        public int ValueId { get; set; }
        public int? ValueTable { get; set; }
        public int? ValueField { get; set; }
        public int? ValueRecord { get; set; }
        public object? ValueData { get; set; }
        public int? ValueReference { get; set; }
        public bool? ValueInvisible { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblField? ValueFieldNavigation { get; set; }
        public virtual TblRecord? ValueRecordNavigation { get; set; }
        public virtual TblRecord? ValueReferenceNavigation { get; set; }
        public virtual TblTable? ValueTableNavigation { get; set; }
    }
}
