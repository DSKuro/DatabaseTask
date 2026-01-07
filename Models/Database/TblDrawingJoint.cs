using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblDrawingJoint
    {
        public TblDrawingJoint()
        {
            TblDrawingJointRelationJointRelationFirstJointNavigations = new HashSet<TblDrawingJointRelation>();
            TblDrawingJointRelationJointRelationSecondJointNavigations = new HashSet<TblDrawingJointRelation>();
        }

        public int JointId { get; set; }
        public int? JointType { get; set; }
        public string? JointLabel { get; set; }
        public double? JointPositionX { get; set; }
        public double? JointPositionY { get; set; }
        public double? JointPositionZ { get; set; }
        public bool? JointActual { get; set; }
        public int? JointDevice { get; set; }
        public int? JointCode { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblDevice? JointDeviceNavigation { get; set; }
        public virtual ICollection<TblDrawingJointRelation> TblDrawingJointRelationJointRelationFirstJointNavigations { get; set; }
        public virtual ICollection<TblDrawingJointRelation> TblDrawingJointRelationJointRelationSecondJointNavigations { get; set; }
    }
}
