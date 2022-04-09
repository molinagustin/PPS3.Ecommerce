using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Provincia
    {
        public int IdProvincia { get; set; }
        [Required(ErrorMessage = "Se debe introducir un Nombre para la Provincia.")]
        public string NombreProv { get; set; } = string.Empty;
    }
}
