using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblSection
    {
        public int SectionId { get; set; }
        public int? SectionProjectVersion { get; set; }
        public int? SectionParent { get; set; }
        public string? SectionName { get; set; }
        public int? SectionColor { get; set; }
        public bool? SectionActual { get; set; }

        public virtual TblProjectVersion? SectionProjectVersionNavigation { get; set; }
    }
}
