using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblWire
    {
        public TblWire()
        {
            TblWireVertices = new HashSet<TblWireVertex>();
        }

        public int WireId { get; set; }
        public int WireInstance { get; set; }
        public int WireProjectVersion { get; set; }
        public int WireEquipment { get; set; }
        public int WireType { get; set; }
        public int WirePhase { get; set; }
        public double? WireWidth { get; set; }
        public double? WireHeight { get; set; }
        public double? WireGause { get; set; }
        public double? WireDiameter { get; set; }
        public bool? WireActual { get; set; }

        public virtual TblProjectVersion WireProjectVersionNavigation { get; set; } = null!;
        public virtual ICollection<TblWireVertex> TblWireVertices { get; set; }
    }
}
