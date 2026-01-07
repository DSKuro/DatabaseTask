namespace DatabaseTask
{
    public partial class SelectFieldDatum
    {
        public int ValueId { get; set; }
        public int? ValueField { get; set; }
        public int? ValueTable { get; set; }
        public string? TableName { get; set; }
        public object? ValueData { get; set; }
    }
}
