using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class EncabezadoRecibo
    {
        public int IdRecibo { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar el Cliente del Recibo")]
        public int IdCliente { get; set; }
        //Le dejo DateTime a pesar que en la BD es DATE porque es mas facil el uso de ese modo y no afecta al guardar o traer datos
        [Required(ErrorMessage = "Se debe colocar la Fecha del Recibo")]
        public DateTime FechaRecibo { get; set; }
        [Required(ErrorMessage = "Se debe colocar la Forma de Pago")]
        public int FormaPago { get; set; }
        public int Tarjeta { get; set; }
        public string NumTarjeta { get; set; } = string.Empty;
        [Required(ErrorMessage = "El importe del Recibo debe indicarse")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ImporteTotal { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
    }
}