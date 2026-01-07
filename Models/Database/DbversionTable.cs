namespace DatabaseTask.Models.Database
{
    public partial class DbversionTable
    {
        public int? CurrentVersion { get; set; }
        public short StructureNo { get; set; }
        public short VersionNo { get; set; }
    }
}
