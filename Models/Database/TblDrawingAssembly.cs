using System;

namespace DatabaseTask
{
    public partial class TblDrawingAssembly
    {
        public int AssemblyId { get; set; }
        public string? AssemblyName { get; set; }
        public Guid EntityKey { get; set; }
    }
}
