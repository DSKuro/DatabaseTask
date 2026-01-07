using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblUserDeviceParameter
    {
        public int DeviceParameterId { get; set; }
        public string? DeviceParameterType { get; set; }
        public int? DeviceParameterTable { get; set; }
        public string? DeviceParameterName { get; set; }
        public bool? IsEquipment { get; set; }
        public bool? IsAssembly { get; set; }
        public bool? IsDetail { get; set; }
        public bool? IsFuncBlock { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblTable? DeviceParameterTableNavigation { get; set; }
    }
}
