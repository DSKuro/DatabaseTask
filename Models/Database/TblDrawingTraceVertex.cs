namespace DatabaseTask
{
    public partial class TblDrawingTraceVertex
    {
        public int TraceVertexId { get; set; }
        public int? TraceVertexTrace { get; set; }
        public double? TraceVertexX { get; set; }
        public double? TraceVertexY { get; set; }
        public double? TraceVertexZ { get; set; }
        public bool? TraceVertexActual { get; set; }

        public virtual TblDrawingTrace? TraceVertexTraceNavigation { get; set; }
    }
}
