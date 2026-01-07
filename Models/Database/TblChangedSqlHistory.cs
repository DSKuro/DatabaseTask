namespace DatabaseTask
{
    public partial class TblChangedSqlHistory
    {
        public long Id { get; set; }
        public short VersionNo { get; set; }
        public string SqlText { get; set; } = null!;
    }
}
