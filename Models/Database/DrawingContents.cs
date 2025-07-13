using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseTask.Models.Database
{
    [Table("dbo.tblDrawingContents")]
    public class DrawingContents
    {
        [Required]
        public int contentId { get; set; }
        public int contentType { get; set; }
        public string contentDocument { get; set; }
        public float contentAngleHorizontal { get; set; }
        public float contentAngleVertical { get; set; }
        public bool contentActual { get; set; } 
        public int contentDevice { get; set; }
        public bool contentMirrored { get; set; }
        public int contentScheme { get; set; }
        [Required]
        public float contentHandleX { get; set; }
        [Required]
        public float contentHandleY { get; set; }
        [Required]
        public float contentLocationMinX { get; set; }
        [Required]
        public float contentLocationMinY { get; set; }
        [Required]
        public float contentLocationMaxX { get; set; }
        [Required]
        public float contentLocationMaxY { get; set; }
        [Required]
        public Guid EntityKey { get; set; }
        public float ContentAngleZ { get; set; }
        [Required]
        public float ContentHandleZ { get; set; }
        [Required]
        public float ContentLocationMaxZ { get; set; }
        [Required]
        public float ContentLocationMinZ { get; set; }
    }
}
