using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblRecord
    {
        public TblRecord()
        {
            TblDeviceCategoryParameters = new HashSet<TblDeviceCategoryParameter>();
        }

        public int RecordId { get; set; }
        public int? RecordTable { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblTable? RecordTableNavigation { get; set; }
        public virtual ICollection<TblDeviceCategoryParameter> TblDeviceCategoryParameters { get; set; }
    }
}
