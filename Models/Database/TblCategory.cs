using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblDeviceCategoryParameters = new HashSet<TblDeviceCategoryParameter>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? CategoryCustomer { get; set; }
        public Guid EntityKey { get; set; }

        public virtual ICollection<TblDeviceCategoryParameter> TblDeviceCategoryParameters { get; set; }
    }
}
