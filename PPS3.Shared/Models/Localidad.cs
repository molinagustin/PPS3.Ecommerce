using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Localidad
    {
        public int IdLocalidad { get; set; }
        [Required]
        public string NombreLoc { get; set; } = string.Empty;
        [Required]
        public int Provincia { get; set; }
        [Required]
        public string CP { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
