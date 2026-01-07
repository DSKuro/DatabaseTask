namespace DatabaseTask.Models.Database
{
    public partial class GetCategoryDatum
    {
        public int DeviceCategoryParameterId { get; set; }
        public int? DeviceCategoryParameterCategory { get; set; }
        public int? DeviceCategoryParameterParameter { get; set; }
        public int? DeviceCategoryParameterOrder { get; set; }
        public int? DeviceCategoryParameterReference { get; set; }
        public object? DeviceCategoryParameterContent { get; set; }
        public object? ReferenceContent { get; set; }
        public bool? DeviceCategoryParameterIsRequired { get; set; }
    }
}
