using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblWireVertex
    {
        public int VertexId { get; set; }
        public int VertexWire { get; set; }
        public double VertexX { get; set; }
        public double VertexY { get; set; }
        public double VertexZ { get; set; }

        public virtual TblWire VertexWireNavigation { get; set; } = null!;
    }
}
