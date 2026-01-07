using System;

namespace DatabaseTask
{
    public partial class TblAccessoryMatrix
    {
        public int AccessoryMatrixId { get; set; }
        public int? AccessoryMatrixDevice { get; set; }
        public int? AccessoryMatrixFrom { get; set; }
        public int? AccessoryMatrixTo { get; set; }
        public int? AccessoryMatrixType { get; set; }
        public bool? AccessoryMatrixActual { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblDevice? AccessoryMatrixDeviceNavigation { get; set; }
        public virtual TblDeviceAccessory? AccessoryMatrixFromNavigation { get; set; }
        public virtual TblDeviceAccessory? AccessoryMatrixToNavigation { get; set; }
    }
}
