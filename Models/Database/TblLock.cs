using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblLock
    {
        public int LockId { get; set; }
        public int? LockRecord { get; set; }
        public int? LockUser { get; set; }
        public DateTime? LockTime { get; set; }
    }
}
