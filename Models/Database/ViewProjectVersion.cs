using System;
using System.Collections.Generic;

namespace DatabaseTask.Models.Database
{
    public partial class ViewProjectVersion
    {
        public int ProjectVersionId { get; set; }
        public int ProjectVersionProject { get; set; }
        public int ProjectVersionNumber { get; set; }
        public DateTime ProjectVersionDate { get; set; }
        public int ProjectVersionUser { get; set; }
        public string? ProjectVersionRemark { get; set; }
        public string? ProjectVersionUserName { get; set; }
        public string? ProjectName { get; set; }
    }
}
