namespace DatabaseTask
{
    public partial class TblDecoration
    {
        public int DecorationId { get; set; }
        public int DecorationInstance { get; set; }
        public int DecorationProjectVersion { get; set; }
        public int DecorationScheme { get; set; }
        public bool? DecorationActual { get; set; }

        public virtual TblProjectVersion DecorationProjectVersionNavigation { get; set; } = null!;
    }
}
