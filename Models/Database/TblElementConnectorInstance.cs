using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblElementConnectorInstance
    {
        public int ConnectorInstanceId { get; set; }
        public int ConnectorInstanceInstance { get; set; }
        public int ConnectorInstanceComponentInstance { get; set; }
        public int ConnectorInstanceConnector { get; set; }

        public virtual TblElementComponentInstance ConnectorInstanceComponentInstanceNavigation { get; set; } = null!;
    }
}
