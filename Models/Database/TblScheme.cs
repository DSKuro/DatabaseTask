using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblScheme
    {
        public TblScheme()
        {
            TblLinkGroups = new HashSet<TblLinkGroup>();
            TblLinks = new HashSet<TblLink>();
        }

        public int SchemeId { get; set; }
        public int SchemeProjectVersion { get; set; }
        public int SchemeType { get; set; }

        public virtual TblProjectVersion SchemeProjectVersionNavigation { get; set; } = null!;
        public virtual ICollection<TblLinkGroup> TblLinkGroups { get; set; }
        public virtual ICollection<TblLink> TblLinks { get; set; }
    }
}
