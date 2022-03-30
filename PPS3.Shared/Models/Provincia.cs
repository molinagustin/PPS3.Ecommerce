using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Provincia
    {
        public int IdProvincia { get; set; }
        [Required(ErrorMessage = "Se debe introducir un Nombre para la Provincia.")]
        public string NombreProv { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
