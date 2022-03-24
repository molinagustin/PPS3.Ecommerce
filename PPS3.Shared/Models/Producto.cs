using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        [Required]
        public string NombreProd { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        public int Rubro { get; set; }
        [Required]
        public int TipoProd { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal PrecioCosto { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal PrecioFinal { get; set; }
        [Required]
        public int Proveedor { get; set; }
        [Required]
        public int UnidadMedida { get; set; }
        [Column(TypeName = "decimal(11,2)")]
        public decimal CantMinAlerta { get; set; }
        public bool Stockeable { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
