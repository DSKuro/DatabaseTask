using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblDrawingTrace
    {
        public TblDrawingTrace()
        {
            TblDrawingTraceVertices = new HashSet<TblDrawingTraceVertex>();
        }

        public int TraceId { get; set; }
        public int? TraceSolution { get; set; }
        public bool? TraceActual { get; set; }

        public virtual TblDrawingSolution? TraceSolutionNavigation { get; set; }
        public virtual ICollection<TblDrawingTraceVertex> TblDrawingTraceVertices { get; set; }
    }
}
