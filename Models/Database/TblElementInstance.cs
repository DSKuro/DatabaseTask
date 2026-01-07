using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblElementInstance
    {
        public TblElementInstance()
        {
            TblElementComponentInstances = new HashSet<TblElementComponentInstance>();
        }

        public int ElementInstanceId { get; set; }
        public int ElementInstanceProjectVersion { get; set; }
        public int ElementInstanceInstance { get; set; }
        public int ElementInstanceElement { get; set; }
        public double? ElementInstanceHandleX { get; set; }
        public double? ElementInstanceHandleY { get; set; }
        public double? ElementInstanceHandleZ { get; set; }
        public double? ElementInstancePositionX { get; set; }
        public double? ElementInstancePositionY { get; set; }
        public double? ElementInstancePositionZ { get; set; }
        public double? ElementInstanceAngleXy { get; set; }
        public double? ElementInstanceAngleYz { get; set; }
        public double? ElementInstanceAngleXz { get; set; }
        public int ElementInstanceGroup { get; set; }
        public int ElementInstanceSection { get; set; }
        public int? ElementInstanceParent { get; set; }
        public int? ElementInstanceAccessoryQuantity { get; set; }
        public bool? ElementInstanceActual { get; set; }
        public string? ElementInstanceSignature { get; set; }
        public bool? ElementInstanceSignatureAuto { get; set; }

        public virtual TblElement ElementInstanceElementNavigation { get; set; } = null!;
        public virtual ICollection<TblElementComponentInstance> TblElementComponentInstances { get; set; }
    }
}
