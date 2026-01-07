using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblElementComponentInstance
    {
        public TblElementComponentInstance()
        {
            TblElementConnectorInstances = new HashSet<TblElementConnectorInstance>();
        }

        public int ComponentInstanceId { get; set; }
        public int ComponentInstanceInstance { get; set; }
        public int ComponentInstanceElementInstance { get; set; }
        public int ComponentInstanceComponent { get; set; }
        public string ComponentInstanceSignature { get; set; } = null!;
        public double ComponentInstanceTransformationHandleX { get; set; }
        public double ComponentInstanceTransformationHandleY { get; set; }
        public double ComponentInstanceTransformationPositionX { get; set; }
        public double ComponentInstanceTransformationPositionY { get; set; }
        public double ComponentInstanceTransformationAngle { get; set; }
        public bool? ComponentInstanceUsed { get; set; }

        public virtual TblElementInstance ComponentInstanceElementInstanceNavigation { get; set; } = null!;
        public virtual ICollection<TblElementConnectorInstance> TblElementConnectorInstances { get; set; }
    }
}
