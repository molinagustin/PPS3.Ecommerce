using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class ProductoListado
    {
        public int IdProducto { get; set; }
        public string? NombreProd { get; set; }
        public string? Descripcion { get; set; }
        public string? DescRubro { get; set; }
        public string? DescripcionTipo { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PrecioCosto { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PrecioFinal { get; set; }
        public string? NombreProv { get; set; }
        public string? DescripcionUnidad { get; set; }
        [Column(TypeName = "decimal(11,2)")]
        public decimal CantMinAlerta { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal StockExistencia { get; set; }
        public string? UsuarioCrea { get; set; }
        public string? UsuarioModif { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }

        //Propiedades para los presupuestos
        [RegularExpression(@"^\d+(?:[,]\d{0,2})?$", ErrorMessage = "La cantidad no es correcta")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Bonificacion { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal BonificacionTotal { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }

        //Para guardar una imagen del producto
        public byte[]? ImagenDestacada { get; set; }
        public string UrlImagen { get; set; } = string.Empty;
    }
}