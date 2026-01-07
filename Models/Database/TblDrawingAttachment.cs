namespace DatabaseTask
{
    public partial class TblDrawingAttachment
    {
        public int AttachmentId { get; set; }
        public int AttachmentDevice { get; set; }
        public string? AttachmentDocument { get; set; }

        public virtual TblDevice AttachmentDeviceNavigation { get; set; } = null!;
    }
}
