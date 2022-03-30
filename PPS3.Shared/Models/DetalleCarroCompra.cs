using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class DetalleCarroCompra
    {
        public int IdDetalle { get; set; }
        public int Carro { get; set; }
        public int Producto { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required(ErrorMessage = "Se debe cargar la Cantidad del producto seleccionado.")]
        public decimal Cantidad { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Descuento { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}