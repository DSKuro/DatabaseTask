namespace DatabaseTask
{
    public partial class TblBusVertex
    {
        public int VertexId { get; set; }
        public int VertexBus { get; set; }
        public double VertexX { get; set; }
        public double VertexY { get; set; }
        public double VertexZ { get; set; }

        public virtual TblBuse VertexBusNavigation { get; set; } = null!;
    }
}
