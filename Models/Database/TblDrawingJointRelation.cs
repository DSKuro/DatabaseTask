using System;

namespace DatabaseTask
{
    public partial class TblDrawingJointRelation
    {
        public int JointRelationId { get; set; }
        public int JointRelationFirstJoint { get; set; }
        public int JointRelationSecondJoint { get; set; }
        public int JointRelationPlacement { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblDrawingJoint JointRelationFirstJointNavigation { get; set; } = null!;
        public virtual TblDrawingJoint JointRelationSecondJointNavigation { get; set; } = null!;
    }
}
