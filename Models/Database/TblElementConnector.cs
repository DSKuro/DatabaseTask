using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblElementConnector
    {
        public int ConnectorId { get; set; }
        public int ConnectorComponent { get; set; }
        public int? ConnectorCompatibility { get; set; }
        public int ConnectorType { get; set; }
        public int ConnectorDirection { get; set; }
        public string ConnectorLabelContent { get; set; } = null!;
        public double ConnectorLabelDirection { get; set; }
        public int ConnectorLabelOrientation { get; set; }
        public double ConnectorPositionX { get; set; }
        public double ConnectorPositionY { get; set; }
        public double ConnectorPositionLabelX { get; set; }
        public double ConnectorPositionLabelY { get; set; }

        public virtual TblElementComponent ConnectorComponentNavigation { get; set; } = null!;
    }
}
