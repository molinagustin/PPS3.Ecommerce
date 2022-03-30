using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class CuerpoComprobante
    {
        public int IdCuerpo { get; set; }
        public int IdEncab { get; set; }
        [Required(ErrorMessage = "Se debe introducir el Producto del Cuerpo Comprobante.")]
        public int ProductoCuerpo { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required(ErrorMessage = "Se debe agregar la Cantidad de Producto.")]
        public decimal CantidadProdC { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Bonificacion { get; set; }
        public int Iva { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal BonificacionTotal { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
    }
}
