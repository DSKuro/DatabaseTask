using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblDevice
    {
        public TblDevice()
        {
            TblAccessoryMatrices = new HashSet<TblAccessoryMatrix>();
            TblDeviceAccessoryDeviceAccessoryMasterNavigations = new HashSet<TblDeviceAccessory>();
            TblDeviceAccessoryDeviceAccessorySlaveNavigations = new HashSet<TblDeviceAccessory>();
            TblDrawingAttachments = new HashSet<TblDrawingAttachment>();
            TblDrawingConnectors = new HashSet<TblDrawingConnector>();
            TblDrawingContents = new HashSet<TblDrawingContent>();
            TblDrawingJoints = new HashSet<TblDrawingJoint>();
            TblDrawingRelationRelationAssemblyNavigations = new HashSet<TblDrawingRelation>();
            TblDrawingRelationRelationDeviceNavigations = new HashSet<TblDrawingRelation>();
            TblDrawingSolutions = new HashSet<TblDrawingSolution>();
            TblDrawingUnits = new HashSet<TblDrawingUnit>();
        }

        public int DeviceId { get; set; }
        public int DeviceType { get; set; }
        public Guid EntityKey { get; set; }
        public string Code { get; set; } = null!;
        public int? TypeDeviceId { get; set; }
        public int? TblRelativeAssemblyId { get; set; }
        public string? Accessories { get; set; }
        public string? UserParams { get; set; }

        public virtual TblRelativeAssembly? TblRelativeAssembly { get; set; }
        public virtual TblTypeDevice? TypeDevice { get; set; }
        public virtual ICollection<TblAccessoryMatrix> TblAccessoryMatrices { get; set; }
        public virtual ICollection<TblDeviceAccessory> TblDeviceAccessoryDeviceAccessoryMasterNavigations { get; set; }
        public virtual ICollection<TblDeviceAccessory> TblDeviceAccessoryDeviceAccessorySlaveNavigations { get; set; }
        public virtual ICollection<TblDrawingAttachment> TblDrawingAttachments { get; set; }
        public virtual ICollection<TblDrawingConnector> TblDrawingConnectors { get; set; }
        public virtual ICollection<TblDrawingContent> TblDrawingContents { get; set; }
        public virtual ICollection<TblDrawingJoint> TblDrawingJoints { get; set; }
        public virtual ICollection<TblDrawingRelation> TblDrawingRelationRelationAssemblyNavigations { get; set; }
        public virtual ICollection<TblDrawingRelation> TblDrawingRelationRelationDeviceNavigations { get; set; }
        public virtual ICollection<TblDrawingSolution> TblDrawingSolutions { get; set; }
        public virtual ICollection<TblDrawingUnit> TblDrawingUnits { get; set; }
    }
}
