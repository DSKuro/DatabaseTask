using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblField
    {
        public int FieldId { get; set; }
        public int? FieldStructure { get; set; }
        public int? FieldTable { get; set; }
        public string? FieldName { get; set; }
        public int? FieldReference { get; set; }
        public bool? FieldPublic { get; set; }
        public int? FieldOrder { get; set; }
        public int? FieldSize { get; set; }
        public double? FieldMin { get; set; }
        public double? FieldMax { get; set; }
        public string? FieldType { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblTable? FieldTableNavigation { get; set; }
    }
}
