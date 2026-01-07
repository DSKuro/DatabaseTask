using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblProjectRelation
    {
        public int Id { get; set; }
        public int? TblRelativeAssemblyId { get; set; }
        public int? RelationDeviceId { get; set; }
        public double? RelationPositionX { get; set; }
        public double? RelationPositionY { get; set; }
        public double? RelationPositionZ { get; set; }
        public double? RelationAngleXy { get; set; }
        public double? RelationAngleXz { get; set; }
        public double? RelationAngleYz { get; set; }
        public bool? IsActual { get; set; }
        public bool? IsAssembly { get; set; }
        public Guid EntityKey { get; set; }
        public int? ArrayId { get; set; }
        public int? DbId { get; set; }
        public string? LocalName { get; set; }

        public virtual TblArray? Array { get; set; }
        public virtual TblRelativeAssembly? TblRelativeAssembly { get; set; }
    }
}
