using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class MovimientoCuentaCorriente
    {
        public int IdCCMov { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ImporteACuenta { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ImporteAbonado { get; set; }
        public DateTime FechaCrea { get; set; }
    }
}