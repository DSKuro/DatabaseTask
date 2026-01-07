using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblPropertyValue
    {
        public int PropertyValueId { get; set; }
        public int PropertyValueProjectVersion { get; set; }
        public int PropertyValueInstance { get; set; }
        public int PropertyValueProperty { get; set; }
        public object? PropertyValueContent { get; set; }
        public bool? PropertyValueActual { get; set; }

        public virtual TblProjectVersion PropertyValueProjectVersionNavigation { get; set; } = null!;
    }
}
