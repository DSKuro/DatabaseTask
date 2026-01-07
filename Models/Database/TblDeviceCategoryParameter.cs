using System;

namespace DatabaseTask
{
    public partial class TblDeviceCategoryParameter
    {
        public int DeviceCategoryParameterId { get; set; }
        public int? DeviceCategoryParameterCategory { get; set; }
        public int? DeviceCategoryParameterParameter { get; set; }
        public int? DeviceCategoryParameterOrder { get; set; }
        public int? DeviceCategoryParameterReference { get; set; }
        public object? DeviceCategoryParameterContent { get; set; }
        public bool? DeviceCategoryParameterIsRequired { get; set; }
        public Guid EntityKey { get; set; }

        public virtual TblCategory? DeviceCategoryParameterCategoryNavigation { get; set; }
        public virtual TblRecord? DeviceCategoryParameterReferenceNavigation { get; set; }
    }
}
