using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class DetallePresupuesto
    {
        public int IdCuerpoPres { get; set; }
        public int IdEncab { get; set; }
        public int Producto { get; set; }
        public string? NombreProducto { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; }
        public string? DescripcionUnidad { get; set; }
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