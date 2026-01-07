using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblTreePath
    {
        public int Id { get; set; }
        public int? IdAncestor { get; set; }
        public int? IdDescendant { get; set; }
    }
}
