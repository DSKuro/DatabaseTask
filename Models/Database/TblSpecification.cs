using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblSpecification
    {
        public int SpecificationId { get; set; }
        public string? SpecificationCode { get; set; }
        public string? SpecificationName { get; set; }
        public string? SpecificationMeasure { get; set; }
        public int? SpecificationRemainder { get; set; }
        public int? SpecificationDelivery { get; set; }
        public double? SpecificationPrice { get; set; }
        public string? SpecificationRemark { get; set; }
    }
}
