using System;
using System.Collections.Generic;

namespace DatabaseTask
{
    public partial class TblProject
    {
        public TblProject()
        {
            TblProjectVersions = new HashSet<TblProjectVersion>();
        }

        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectRemark { get; set; }
        public string? ProjectIp { get; set; }
        public double? ProjectDimensionsHeight { get; set; }
        public double? ProjectAmperageShortCircuit { get; set; }
        public string? ProjectSectioning { get; set; }
        public string? ProjectDeveloperShort { get; set; }
        public string? ProjectCustomer { get; set; }
        public string? ProjectTask { get; set; }
        public string? ProjectStage { get; set; }
        public string? ProjectApprover { get; set; }
        public int? ProjectReleaseYear { get; set; }
        public int? EquipmentInstallOrientation { get; set; }
        public int? ProjectCabinetService { get; set; }
        public double? ProjectSimultaneity { get; set; }
        public int? DoorsType { get; set; }
        public int? ProjectGaugePe { get; set; }
        public int? BusHardness { get; set; }
        public string? ProjectVersionUser { get; set; }
        public DateTime? DateEdit { get; set; }
        public int? TblRelativeAssemblyNavigationId { get; set; }
        public int RelativeAssemblyId { get; set; }
        public string? Geometry2d { get; set; }
        public string? Accessories { get; set; }
        public string? SaveScheme { get; set; }

        public virtual TblRelativeAssembly? TblRelativeAssemblyNavigation { get; set; }
        public virtual ICollection<TblProjectVersion> TblProjectVersions { get; set; }
    }
}
