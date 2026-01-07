using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblDrawingConductor
    {
        public TblDrawingConductor()
        {
            TblDrawingConductorWires = new HashSet<TblDrawingConductorWire>();
        }

        public int ConductorId { get; set; }
        public int? ConductorDevice { get; set; }
        public int? ConductorPhases { get; set; }
        public int? ConductorWires { get; set; }
        public double? ConductorWidth { get; set; }
        public double? ConductorHeight { get; set; }
        public double? ConductorAngle { get; set; }

        public virtual ICollection<TblDrawingConductorWire> TblDrawingConductorWires { get; set; }
    }
}
