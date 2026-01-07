using System;

namespace DatabaseTask
{
    public partial class TblDrawingContent
    {
        public int ContentId { get; set; }
        public int? ContentType { get; set; }
        public string? ContentDocument { get; set; }
        public double? ContentAngleHorizontal { get; set; }
        public double? ContentAngleVertical { get; set; }
        public bool? ContentActual { get; set; }
        public int? ContentDevice { get; set; }
        public bool? ContentMirrored { get; set; }
        public int? ContentScheme { get; set; }
        public double ContentHandleX { get; set; }
        public double ContentHandleY { get; set; }
        public double ContentLocationMinX { get; set; }
        public double ContentLocationMinY { get; set; }
        public double ContentLocationMaxX { get; set; }
        public double ContentLocationMaxY { get; set; }
        public Guid EntityKey { get; set; }
        public double? ContentAngleZ { get; set; }
        public double ContentHandleZ { get; set; }
        public double ContentLocationMaxZ { get; set; }
        public double ContentLocationMinZ { get; set; }

        public virtual TblDevice? ContentDeviceNavigation { get; set; }
    }
}
