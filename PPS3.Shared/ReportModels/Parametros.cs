namespace PPS3.Shared.ReportModels
{
    public class Parametros
    {
        public int Top { get; set; }
        public bool RangoFechas { get; set; }
        public DateTime FechaDesde { get; set; } = DateTime.Today;
        public DateTime FechaHasta { get; set; } = DateTime.Today;
        public bool CC { get; set; }
        public int TipoVta { get; set; }
        public bool UsarBonif { get; set; }
        public bool ConBonif { get; set; }
        public bool CompPagos { get; set; }
        public bool UsarFP { get; set; }
        public int FormaPago { get; set; }
    }
}
