using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblHistoryJointRelation
    {
        public int? HistoryOperation { get; set; }
        public int? HistoryJointFirst { get; set; }
        public int? HistoryJointSecond { get; set; }
        public DateTime? HistoryTime { get; set; }
    }
}
