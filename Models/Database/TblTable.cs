using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblTable
    {
        public TblTable()
        {
            TblDeviceParameters = new HashSet<TblDeviceParameter>();
            TblFields = new HashSet<TblField>();
            TblRecords = new HashSet<TblRecord>();
            TblUserDeviceParameters = new HashSet<TblUserDeviceParameter>();
        }

        public int TableId { get; set; }
        public int? TableStructure { get; set; }
        public int? TableCluster { get; set; }
        public string? TableName { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblCluster? TableClusterNavigation { get; set; }
        public virtual ICollection<TblDeviceParameter> TblDeviceParameters { get; set; }
        public virtual ICollection<TblField> TblFields { get; set; }
        public virtual ICollection<TblRecord> TblRecords { get; set; }
        public virtual ICollection<TblUserDeviceParameter> TblUserDeviceParameters { get; set; }
    }
}
