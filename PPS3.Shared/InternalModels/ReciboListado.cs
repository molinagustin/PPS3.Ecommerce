using PPS3.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class ReciboListado
    {
        public int IdRecibo { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaRecibo { get; set; }
        public string FormaP { get; set; } = string.Empty;
        public string NombreTarj { get; set; } = string.Empty;
        public string NumTarjeta { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal ImporteTotal { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string UsuarioCrea { get; set; } = string.Empty;
        public DateTime FechaCrea { get; set; }

        public ICollection<DetalleRecibo>? DetallesRecibo { get; set; }
    }
}
