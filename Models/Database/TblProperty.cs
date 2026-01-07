using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblProperty
    {
        public int PropertyId { get; set; }
        public int PropertyProjectVersion { get; set; }
        public int PropertyProperty { get; set; }
        public string PropertyName { get; set; } = null!;
        public bool? PropertyActual { get; set; }

        public virtual TblProjectVersion PropertyProjectVersionNavigation { get; set; } = null!;
    }
}
