using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblLinkGroup
    {
        public int LinkGroupId { get; set; }
        public int LinkGroupInstance { get; set; }
        public int LinkGroupProjectVersion { get; set; }
        public int LinkGroupScheme { get; set; }
        public string? LinkGroupLabel { get; set; }
        public bool? LinkGroupActual { get; set; }

        public virtual TblScheme LinkGroupSchemeNavigation { get; set; } = null!;
    }
}
