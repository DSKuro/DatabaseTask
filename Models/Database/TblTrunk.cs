using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblTrunk
    {
        public TblTrunk()
        {
            TblTrunkBindings = new HashSet<TblTrunkBinding>();
        }

        public int TrunkId { get; set; }
        public int TrunkInstance { get; set; }
        public int TrunkProjectVersion { get; set; }
        public int TrunkScheme { get; set; }
        public double TrunkTransformationHandleX { get; set; }
        public double TrunkTransformationHandleY { get; set; }
        public double TrunkTransformationPositionX { get; set; }
        public double TrunkTransformationPositionY { get; set; }
        public double TrunkTransformationAngle { get; set; }
        public bool? TrunkActual { get; set; }

        public virtual TblProjectVersion TrunkProjectVersionNavigation { get; set; } = null!;
        public virtual ICollection<TblTrunkBinding> TblTrunkBindings { get; set; }
    }
}
