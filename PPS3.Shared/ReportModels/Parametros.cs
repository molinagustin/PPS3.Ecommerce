namespace PPS3.Shared.ReportModels
{
    public class Parametros
    {
        public int Top { get; set; } = 5;
        public bool RangoFechas { get; set; } = false;
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public bool CC { get; set; } = false;
        public int TipoVta { get; set; } = 1;
        public bool UsarBonif { get; set; } = false; 
        public bool ConBonif { get; set; } = false; 
        public bool CompPagos { get; set; } = false;
        public bool UsarFP { get; set; } = false;
        public int FormaPago { get; set; } = 1;
        public string FormaPagoStr { get; set; } = string.Empty;
    }
}
