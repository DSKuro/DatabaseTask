using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblRelativeAssembly
    {
        public TblRelativeAssembly()
        {
            TblArrays = new HashSet<TblArray>();
            TblDevices = new HashSet<TblDevice>();
            TblProjectRelations = new HashSet<TblProjectRelation>();
            TblProjects = new HashSet<TblProject>();
        }

        public int Id { get; set; }
        public int? ArrayId { get; set; }

        public virtual ICollection<TblArray> TblArrays { get; set; }
        public virtual ICollection<TblDevice> TblDevices { get; set; }
        public virtual ICollection<TblProjectRelation> TblProjectRelations { get; set; }
        public virtual ICollection<TblProject> TblProjects { get; set; }
    }
}
