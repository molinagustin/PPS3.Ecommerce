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
        public DateTime FechaCrea { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnit { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Bonificacion { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal BonificacionTotal { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }

        //Propiedad interna solo para traer el nombre del producto
        public string? NombreProducto { get; set; }
        public string? DescripcionUnidad { get; set; }
    }
}