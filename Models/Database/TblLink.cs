using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblLink
    {
        public TblLink()
        {
            TblLinkVertices = new HashSet<TblLinkVertex>();
        }

        public int LinkId { get; set; }
        public int LinkInstance { get; set; }
        public int LinkProjectVersion { get; set; }
        public int LinkScheme { get; set; }
        public int LinkGroup { get; set; }
        public int LinkConnectorInstanceFrom { get; set; }
        public int LinkConnectorInstanceTo { get; set; }
        public string? LinkLabel { get; set; }
        public bool? LinkActual { get; set; }

        public virtual TblScheme LinkSchemeNavigation { get; set; } = null!;
        public virtual ICollection<TblLinkVertex> TblLinkVertices { get; set; }
    }
}
