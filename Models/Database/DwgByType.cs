namespace DatabaseTask.Models.Database
{
    public partial class DwgByType
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Path { get; set; } = null!;
    }
}
