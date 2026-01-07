using System;

namespace DatabaseTask
{
    public partial class TblDeviceParameter
    {
        public int DeviceParameterId { get; set; }
        public string? DeviceParameterType { get; set; }
        public int? DeviceParameterTable { get; set; }
        public string? DeviceParameterName { get; set; }
        public bool? DeviceParameterIsEquipment { get; set; }
        public bool? DeviceParameterIsAccessory { get; set; }
        public bool? DeviceParameterIsCabinet { get; set; }
        public bool? DeviceParameterIsAssembly { get; set; }
        public bool? DeviceParameterIsBus { get; set; }
        public bool? DeviceParameterIsBusAssembly { get; set; }
        public Guid EntityKey { get; set; }
        public bool? IsAssembly { get; set; }
        public bool? IsDetail { get; set; }
        public bool? IsEquipment { get; set; }
        public bool? IsFuncBlock { get; set; }

        public virtual TblTable? DeviceParameterTableNavigation { get; set; }
    }
}
