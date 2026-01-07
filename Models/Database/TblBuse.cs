using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblBuse
    {
        public TblBuse()
        {
            TblBusVertices = new HashSet<TblBusVertex>();
        }

        public int BusId { get; set; }
        public int? BusProjectVersion { get; set; }
        public int? BusInstance { get; set; }
        public double? BusWidth { get; set; }
        public double? BusHeight { get; set; }
        public double? BusCurrent { get; set; }
        public int? BusPerPhase { get; set; }
        public int? BusPoluses { get; set; }
        public bool? BusActual { get; set; }

        public virtual TblProjectVersion? BusProjectVersionNavigation { get; set; }
        public virtual ICollection<TblBusVertex> TblBusVertices { get; set; }
    }
}
