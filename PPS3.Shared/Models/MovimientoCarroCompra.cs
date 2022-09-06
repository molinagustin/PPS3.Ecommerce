namespace PPS3.Shared.Models
{
    public class MovimientoCarroCompra
    {
        public int IdMovimiento { get; set; }
        public int IdOrden { get; set; }
        public int IdEstado { get; set; }
        public int Usuario { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
