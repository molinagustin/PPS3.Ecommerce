using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class CuerpoPresupuesto
    {
        public int IdCuerpoPres { get; set; }
        public int IdEncab { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar un Producto.")]
        [Column(TypeName = "decimal(10,2)")]
        public int Producto { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar la Cantidad.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnit { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Bonificacion { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal BonificacionTotal { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }
    }
}
