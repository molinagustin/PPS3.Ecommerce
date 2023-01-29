using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class MovimientosPago
    {
        public DateTime FechaRecibo { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ImporteTotal { get; set; }
        public string FormaP { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal SaldoRestante { get; set; }
    }
}
