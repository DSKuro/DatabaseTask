using System;

namespace DatabaseTask
{
    public partial class TblDrawingRelation
    {
        public int RelationId { get; set; }
        public int? RelationAssembly { get; set; }
        public int? RelationDeviceCode { get; set; }
        public int? RelationDevice { get; set; }
        public int? RelationDeviceParentCode { get; set; }
        public bool? RelationIsAccessory { get; set; }
        public int? RelationJointCode { get; set; }
        public int? RelationJointParentCode { get; set; }
        public double? RelationPositionX { get; set; }
        public double? RelationPositionY { get; set; }
        public double? RelationPositionZ { get; set; }
        public double? RelationAngleXy { get; set; }
        public double? RelationAngleXz { get; set; }
        public double? RelationAngleYz { get; set; }
        public int? RelationQuantity { get; set; }
        public bool? RelationActual { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblDevice? RelationAssemblyNavigation { get; set; }
        public virtual TblDevice? RelationDeviceNavigation { get; set; }
    }
}
