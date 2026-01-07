using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblTypeDevice
    {
        public TblTypeDevice()
        {
            TblDevices = new HashSet<TblDevice>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<TblDevice> TblDevices { get; set; }
    }
}
