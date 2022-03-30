namespace PPS3.Shared.Models
{
    public class EstadoCarroCompra
    {
        public int IdEstado { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}