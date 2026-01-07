using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblGroup
    {
        public int GroupId { get; set; }
        public int? GroupProjectVersion { get; set; }
        public int? GroupParent { get; set; }
        public string? GroupName { get; set; }

        public virtual TblProjectVersion? GroupProjectVersionNavigation { get; set; }
    }
}
