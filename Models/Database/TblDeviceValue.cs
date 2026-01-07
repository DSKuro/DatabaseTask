using System;

namespace DatabaseTask
{
    public partial class TblDeviceValue
    {
        public int DeviceValueId { get; set; }
        public int? DeviceValueDevice { get; set; }
        public int? DeviceValueParameter { get; set; }
        public int? DeviceValueReference { get; set; }
        public object? DeviceValueContent { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblDevice? DeviceValueDeviceNavigation { get; set; }
        public virtual TblDeviceParameter? DeviceValueParameterNavigation { get; set; }
        public virtual TblRecord? DeviceValueReferenceNavigation { get; set; }
    }
}
