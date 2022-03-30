namespace PPS3.Shared.Models
{
    public class IngresoCuentaCorriente
    {
        public int IdIngreso { get; set; }
        public int CuentaCorriente { get; set; }
        public int CompIngreso { get; set; }
        public int TipoIngreso { get; set; }
        public int MovimientoCC { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
    }
}
