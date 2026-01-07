using System;

namespace DatabaseTask
{
    public partial class TblDrawingConnector
    {
        public int ConnectorId { get; set; }
        public int? ConnectorDirection { get; set; }
        public string? ConnectorLabel { get; set; }
        public double? ConnectorPositionX { get; set; }
        public double? ConnectorPositionY { get; set; }
        public bool? ConnectorActual { get; set; }
        public int? ConnectorDevice { get; set; }
        public int ConnectorContent { get; set; }
        public double ConnectorLabelX { get; set; }
        public double ConnectorLabelY { get; set; }
        public double ConnectorLabelDirection { get; set; }
        public int ConnectorLabelOrientation { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblDevice? ConnectorDeviceNavigation { get; set; }
    }
}
