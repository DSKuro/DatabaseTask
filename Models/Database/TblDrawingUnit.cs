using System;

namespace DatabaseTask
{
    public partial class TblDrawingUnit
    {
        public int UnitId { get; set; }
        public int? UnitDevice { get; set; }
        public double? UnitBoundMinX { get; set; }
        public double? UnitBoundMaxX { get; set; }
        public double? UnitBoundMinY { get; set; }
        public double? UnitBoundMaxY { get; set; }
        public double? UnitBoundMinZ { get; set; }
        public double? UnitBoundMaxZ { get; set; }
        public double? UnitMountingMinX { get; set; }
        public double? UnitMountingMaxX { get; set; }
        public double? UnitMountingMinY { get; set; }
        public double? UnitMountingMaxY { get; set; }
        public double? UnitMountingMinZ { get; set; }
        public double? UnitMountingMaxZ { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblDevice? UnitDeviceNavigation { get; set; }
    }
}
