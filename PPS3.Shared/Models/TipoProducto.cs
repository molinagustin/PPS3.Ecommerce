using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class TipoProducto
    {
        public int IdTipo { get; set; }
        [Required(ErrorMessage = "El nombre del Tipo de Producto es obligatorio.")]
        public string DescripcionTipo { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
