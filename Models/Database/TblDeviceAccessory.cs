using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblDeviceAccessory
    {
        public TblDeviceAccessory()
        {
            TblAccessoryMatricesAccessoryMatricesFromNavigation = new HashSet<TblAccessoryMatrix>();
            TblAccessoryMatricesAccessoryMatricesToNavigation = new HashSet<TblAccessoryMatrix>();
        }

        public int DeviceAccessoryId { get; set; }
        public int? DeviceAccessoryMaster { get; set; }
        public int? DeviceAccessorySlave { get; set; }
        public int? DeviceAccessoryQuantity { get; set; }
        public bool? DeviceAccessoryActual { get; set; }
        public int? DeviceAccessoryJointMaster { get; set; }
        public int? DeviceAccessoryJointSlave { get; set; }
        public double? DeviceAccessoryAngleXy { get; set; }
        public double? DeviceAccessoryAngleXz { get; set; }
        public double? DeviceAccessoryAngleYz { get; set; }
        public bool? DeviceAccessoryInvisible { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblDevice? DeviceAccessoryMasterNavigation { get; set; }
        public virtual TblDevice? DeviceAccessorySlaveNavigation { get; set; }
        public virtual ICollection<TblAccessoryMatrix> TblAccessoryMatricesAccessoryMatricesFromNavigation { get; set; }
        public virtual ICollection<TblAccessoryMatrix> TblAccessoryMatricesAccessoryMatricesToNavigation { get; set; }
    }
}
