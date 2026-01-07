using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblTrunkBinding
    {
        public int BindingId { get; set; }
        public int BindingInstance { get; set; }
        public int BindingProjectVersion { get; set; }
        public int BindingTrunk { get; set; }
        public int BindingConnector { get; set; }
        public bool? BindingActual { get; set; }

        public virtual TblTrunk BindingTrunkNavigation { get; set; } = null!;
    }
}
