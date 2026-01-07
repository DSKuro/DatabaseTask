using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblProjectVersion
    {
        public TblProjectVersion()
        {
            TblBuses = new HashSet<TblBuse>();
            TblCabinets = new HashSet<TblCabinet>();
            TblDecorations = new HashSet<TblDecoration>();
            TblElements = new HashSet<TblElement>();
            TblGroups = new HashSet<TblGroup>();
            TblProperties = new HashSet<TblProperty>();
            TblPropertyValues = new HashSet<TblPropertyValue>();
            TblSchemes = new HashSet<TblScheme>();
            TblSections = new HashSet<TblSection>();
            TblTrunks = new HashSet<TblTrunk>();
            TblWires = new HashSet<TblWire>();
        }

        public int ProjectVersionId { get; set; }
        public int ProjectVersionProject { get; set; }
        public int ProjectVersionNumber { get; set; }
        public DateTime ProjectVersionDate { get; set; }
        public int ProjectVersionUser { get; set; }
        public string? ProjectVersionRemark { get; set; }
        public bool? ProjectVersionManualChanged { get; set; }
        public string? ProjectVersionUserName { get; set; }
        public bool? ProjectVersionTemporary { get; set; }

        public virtual TblProject ProjectVersionProjectNavigation { get; set; } = null!;
        public virtual ICollection<TblBuse> TblBuses { get; set; }
        public virtual ICollection<TblCabinet> TblCabinets { get; set; }
        public virtual ICollection<TblDecoration> TblDecorations { get; set; }
        public virtual ICollection<TblElement> TblElements { get; set; }
        public virtual ICollection<TblGroup> TblGroups { get; set; }
        public virtual ICollection<TblProperty> TblProperties { get; set; }
        public virtual ICollection<TblPropertyValue> TblPropertyValues { get; set; }
        public virtual ICollection<TblScheme> TblSchemes { get; set; }
        public virtual ICollection<TblSection> TblSections { get; set; }
        public virtual ICollection<TblTrunk> TblTrunks { get; set; }
        public virtual ICollection<TblWire> TblWires { get; set; }
    }
}
