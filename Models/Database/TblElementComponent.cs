using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblElementComponent
    {
        public TblElementComponent()
        {
            TblElementConnectors = new HashSet<TblElementConnector>();
        }

        public int ComponentId { get; set; }
        public int ComponentElement { get; set; }
        public int ComponentSchemeType { get; set; }
        public string ComponentFileName { get; set; } = null!;
        public double ComponentLocationMinX { get; set; }
        public double ComponentLocationMinY { get; set; }
        public double ComponentLocationMaxX { get; set; }
        public double ComponentLocationMaxY { get; set; }
        public double ComponentTransformationHandleX { get; set; }
        public double ComponentTransformationHandleY { get; set; }
        public double ComponentTransformationPositionX { get; set; }
        public double ComponentTransformationPositionY { get; set; }
        public double ComponentTransformationAngle { get; set; }

        public virtual TblElement ComponentElementNavigation { get; set; } = null!;
        public virtual ICollection<TblElementConnector> TblElementConnectors { get; set; }
    }
}
