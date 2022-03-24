using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.InternalModels
{
    public class UsuarioRequest
    {
        [Required]
        public string NombreUs { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
