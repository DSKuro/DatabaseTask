using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblDrawingSolution
    {
        public TblDrawingSolution()
        {
            TblDrawingContainers = new HashSet<TblDrawingContainer>();
            TblDrawingTraces = new HashSet<TblDrawingTrace>();
        }

        public int SolutionId { get; set; }
        public int? SolutionDevice { get; set; }

        public virtual TblDevice? SolutionDeviceNavigation { get; set; }
        public virtual ICollection<TblDrawingContainer> TblDrawingContainers { get; set; }
        public virtual ICollection<TblDrawingTrace> TblDrawingTraces { get; set; }
    }
}
