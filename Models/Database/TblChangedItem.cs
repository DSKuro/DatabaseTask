using System;

namespace DatabaseTask
{
    public partial class TblChangedItem
    {
        public long Id { get; set; }
        public string TblName { get; set; } = null!;
        public byte OpType { get; set; }
        public int TblId { get; set; }
        public short VersionNo { get; set; }
        public DateTime? Dt { get; set; }
        public Guid EntityKey { get; set; }
    }
}
