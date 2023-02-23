using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class OrdenesCompraCotiz
    {
        public int IdCarro { get; set; }
        public string? Estado { get; set; }
        public string? UsuarioCrea { get; set; }
        public DateTime? FechaOrden { get; set; }
        public DateTime? FechaEntrega { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public bool Pagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? Observaciones { get; set; }
        public string? UsGenCot { get; set; }
    }
}
