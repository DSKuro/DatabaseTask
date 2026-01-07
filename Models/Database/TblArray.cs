using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblArray
    {
        public TblArray()
        {
            InverseTblArrayNavigation = new HashSet<TblArray>();
            TblProjectRelations = new HashSet<TblProjectRelation>();
        }

        public int Id { get; set; }
        public int TblRelativeAssemblyId { get; set; }
        public int? Xcount { get; set; }
        public int? Ycount { get; set; }
        public int? Zcount { get; set; }
        public double? Xdirection { get; set; }
        public double? Ydirection { get; set; }
        public double? Zdirection { get; set; }
        public int? TblArrayId { get; set; }
        public string? LocalName { get; set; }

        public virtual TblArray? TblArrayNavigation { get; set; }
        public virtual TblRelativeAssembly TblRelativeAssembly { get; set; } = null!;
        public virtual ICollection<TblArray> InverseTblArrayNavigation { get; set; }
        public virtual ICollection<TblProjectRelation> TblProjectRelations { get; set; }
    }
}
