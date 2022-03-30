using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Genero
    {
        public int IdGenero { get; set; }
        [Required(ErrorMessage = "Se debe dar una Descripcion del Genero.")]
        public string DescGenero { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
