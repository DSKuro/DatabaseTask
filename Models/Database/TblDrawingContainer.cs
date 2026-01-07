namespace DatabaseTask
{
    public partial class TblDrawingContainer
    {
        public int ContainerId { get; set; }
        public int? ContainerDevice { get; set; }
        public int? ContainerSolution { get; set; }
        public string? ContainerRemark { get; set; }
        public double? ContainerHandleX { get; set; }
        public double? ContainerHandleY { get; set; }
        public double? ContainerHandleZ { get; set; }
        public double? ContainerPositionX { get; set; }
        public double? ContainerPositionY { get; set; }
        public double? ContainerPositionZ { get; set; }
        public double? ContainerAngleXy { get; set; }
        public double? ContainerAngleXz { get; set; }
        public double? ContainerAngleYz { get; set; }
        public bool? ContainerActual { get; set; }

        public virtual TblDrawingSolution? ContainerSolutionNavigation { get; set; }
    }
}
