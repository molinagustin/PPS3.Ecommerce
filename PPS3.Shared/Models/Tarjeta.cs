using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    //Si la tarjeta es Maestro, Visa, Cabal, American Express, etc.
    public class Tarjeta
    {
        public int IdTarjeta { get; set; }
        [Required(ErrorMessage = "El Nombre de la Tarjeta debe ser completado.")]
        public string NombreTarj { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe seleccionar un Tipo de Tarjeta")]
        public int TipoTarj { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}