using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        [Required(ErrorMessage = "El Nombre de Producto es un campo obligatorio.")]
        public string NombreProd { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Rubro del producto es obligatorio.")]
        public int Rubro { get; set; }
        [Required(ErrorMessage = "El Tipo de Producto es obligatorio.")]
        public int TipoProd { get; set; }
        [Required(ErrorMessage = "El Precio de Costo debe ser introducido.")]
        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(?:[,]\d{0,2})?$", ErrorMessage = "El precio no es correcto")]
        public decimal PrecioCosto { get; set; }
        [Required(ErrorMessage = "El Precio Final debe ser introducido.")]
        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(?:[,]\d{0,2})?$", ErrorMessage = "El precio no es correcto")]
        public decimal PrecioFinal { get; set; }
        [Required(ErrorMessage = "Se debe introducir un Proveedor.")]
        public int Proveedor { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar la Unidad de Medida correspondiente.")]
        public int UnidadMedida { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Bonificacion { get; set; }
        [Column(TypeName = "decimal(11,2)")]
        public decimal CantMinAlerta { get; set; }
        [Required(ErrorMessage = "El stock es requerido. Al menos en 0")]
        [Column(TypeName = "decimal(8,2)")]
        [RegularExpression(@"^\d+(?:[,]\d{0,2})?$", ErrorMessage = "El stock no es correcto")]
        public decimal StockExistencia { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
        public byte[]? ImagenDestacada { get; set; }
    }
}
