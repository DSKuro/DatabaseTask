namespace DatabaseTask.Models.Database
{
    public partial class GetJoinCategoryAndDevice
    {
        public int? DeviceValueDevice { get; set; }
        public int? DeviceCategoryParameterCategory { get; set; }
        public int DeviceParameterId { get; set; }
        public string? DeviceParameterType { get; set; }
        public int? DeviceParameterTable { get; set; }
        public string? DeviceParameterName { get; set; }
        public int? DeviceValueReference { get; set; }
        public object? DeviceValueContent { get; set; }
        public object? ReferenceContent { get; set; }
        public object? CreferenceContent { get; set; }
    }
}
