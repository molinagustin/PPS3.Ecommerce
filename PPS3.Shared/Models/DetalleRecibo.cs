using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class DetalleRecibo
    {
        public int IdDetalle { get; set; }
        public int IdRecibo { get; set; }
        public int IdComprobante { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Importe { get; set; }
    }
}
