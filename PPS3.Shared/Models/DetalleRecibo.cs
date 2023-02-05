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


        //Propiedades para PDF solamente
        public DateTime FechaComp { get; set; } = DateTime.Now;
        public int Carro { get; set; } = 0;
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ImporteFinal { get; set; } = 0;
        [Column(TypeName = "decimal(10, 2)")]
        public decimal SaldoRestante { get; set; } = 0;
        public bool Pagado { get; set; } = false;
    }
}
