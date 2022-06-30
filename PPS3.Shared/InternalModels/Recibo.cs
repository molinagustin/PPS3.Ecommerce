using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class Recibo
    {
        public int IdRecibo { get; set; }
        public int IdDetalle { get; set; }
        public int IdComprobante { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Importe { get; set; }
        public DateTime FechaRecibo { get; set; }
        public string FormaP { get; set; } = string.Empty;
        public string NombreTarj { get; set; } = string.Empty;
        public string NumTarjeta { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal ImporteTotal { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string UsuarioCrea { get; set; } = string.Empty;
        public DateTime FechaCrea { get; set; }
    }
}
