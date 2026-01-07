using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblElement
    {
        public TblElement()
        {
            TblElementComponents = new HashSet<TblElementComponent>();
            TblElementInstances = new HashSet<TblElementInstance>();
        }

        public int ElementId { get; set; }
        public int? ElementInstance { get; set; }
        public int ElementProjectVersion { get; set; }
        public int ElementDevice { get; set; }
        public int? ElementType { get; set; }
        public string? ElementContentFileName { get; set; }
        public double? ElementContentAngleXy { get; set; }
        public double? ElementContentAngleYz { get; set; }
        public double? ElementContentAngleXz { get; set; }
        public bool? ElementContentMirrored { get; set; }
        public double? ElementBoundMinX { get; set; }
        public double? ElementBoundMinY { get; set; }
        public double? ElementBoundMinZ { get; set; }
        public double? ElementBoundMaxX { get; set; }
        public double? ElementBoundMaxY { get; set; }
        public double? ElementBoundMaxZ { get; set; }
        public double? ElementMountingMinX { get; set; }
        public double? ElementMountingMinY { get; set; }
        public double? ElementMountingMinZ { get; set; }
        public double? ElementMountingMaxX { get; set; }
        public double? ElementMountingMaxY { get; set; }
        public double? ElementMountingMaxZ { get; set; }
        public bool? ElementActual { get; set; }
        public string? ElementAttachmentFileName { get; set; }

        public virtual TblProjectVersion ElementProjectVersionNavigation { get; set; } = null!;
        public virtual ICollection<TblElementComponent> TblElementComponents { get; set; }
        public virtual ICollection<TblElementInstance> TblElementInstances { get; set; }
    }
}
