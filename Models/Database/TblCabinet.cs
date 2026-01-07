namespace DatabaseTask
{
    public partial class TblCabinet
    {
        public int CabinetId { get; set; }
        public int? CabinetProjectVersion { get; set; }
        public int? CabinetInstance { get; set; }
        public string? CabinetSignature { get; set; }
        public int? CabinetPlacement { get; set; }
        public double? CabinetTemperatureMin { get; set; }
        public double? CabinetTemperatureMax { get; set; }
        public bool? CabinetPermanently { get; set; }
        public bool? CabinetActual { get; set; }

        public virtual TblProjectVersion? CabinetProjectVersionNavigation { get; set; }
    }
}
