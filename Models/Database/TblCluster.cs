using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblCluster
    {
        public TblCluster()
        {
            TblTables = new HashSet<TblTable>();
        }

        public int ClusterId { get; set; }
        public int? ClusterStructure { get; set; }
        public string? ClusterName { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblStructure? ClusterStructureNavigation { get; set; }
        public virtual ICollection<TblTable> TblTables { get; set; }
    }
}
