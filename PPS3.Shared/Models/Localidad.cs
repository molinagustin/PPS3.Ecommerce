using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Localidad
    {
        public int IdLocalidad { get; set; }
        [Required(ErrorMessage = "Se debe introducir un Nombre para la Localidad.")]
        public string NombreLoc { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe seleccionar una Provincia para la Localidad.")]
        public int Provincia { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El código postal acepta solo números.")]
        [Required(ErrorMessage = "Se debe introducir el Codigo Postal de la Localidad.")]
        public string CP { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
