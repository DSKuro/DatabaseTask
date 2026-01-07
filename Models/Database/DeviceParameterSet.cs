namespace DatabaseTask.Models.Database
{
    public partial class DeviceParameterSet
    {
        public int DeviceValueId { get; set; }
        public int? DeviceValueDevice { get; set; }
        public int DeviceParameterId { get; set; }
        public string? DeviceParameterType { get; set; }
        public int? DeviceParameterTable { get; set; }
        public string? DeviceParameterName { get; set; }
        public int? DeviceValueReference { get; set; }
        public object? DeviceValueContent { get; set; }
        public object? ReferenceContent { get; set; }
    }
}
