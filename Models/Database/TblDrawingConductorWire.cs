namespace DatabaseTask
{
    public partial class TblDrawingConductorWire
    {
        public int ConductorWireId { get; set; }
        public int? ConductorWireConductor { get; set; }
        public int? ConductorWirePhase { get; set; }
        public double? ConductorWirePositionX { get; set; }
        public double? ConductorWirePositionY { get; set; }
        public bool? ConductorWireActual { get; set; }

        public virtual TblDrawingConductor? ConductorWireConductorNavigation { get; set; }
    }
}
