using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Rubro
    {
        public int IdRubro { get; set; }
        [Required(ErrorMessage = "Se debe introducir una Descripcion del Rubro.")]
        public string DescRubro { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
