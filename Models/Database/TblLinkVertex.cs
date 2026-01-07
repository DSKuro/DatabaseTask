using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblLinkVertex
    {
        public int VertexId { get; set; }
        public int VertexLink { get; set; }
        public int VertexProjectVersion { get; set; }
        public double VertexPositionX { get; set; }
        public double VertexPositionY { get; set; }
        public bool? VertexActual { get; set; }

        public virtual TblLink VertexLinkNavigation { get; set; } = null!;
    }
}
