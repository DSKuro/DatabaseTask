using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblStructure
    {
        public TblStructure()
        {
            TblClusters = new HashSet<TblCluster>();
        }

        public int StructureId { get; set; }
        public int? StructureUser { get; set; }
        public DateTime? StructureDate { get; set; }
        public Guid EntityKey { get; set; }

        public virtual ICollection<TblCluster> TblClusters { get; set; }
    }
}
