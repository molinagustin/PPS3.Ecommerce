using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class Comprobante
    {
        public int IdEncab { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public string NumComp { get; set; } = string.Empty;
        public int ClienteComp { get; set; }
        public string TipoComp { get; set; } = string.Empty;
        public DateTime FechaComp { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string FormaP { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal ImporteFinal { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SaldoRestante { get; set; }
        public bool Pagado { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string UsuarioCrea { get; set; } = string.Empty;
        public DateTime FechaCrea { get; set; }
        public ICollection<DetalleComprobante>? DetallesComprobante { get; set; }
    }
}
