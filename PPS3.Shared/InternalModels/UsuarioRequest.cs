using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.InternalModels
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z]{4,8}$", ErrorMessage = "El nombre de usuario debe contener letras mayusculas y/o minusculas solamente.")]
        public string NombreUs { get; set; } = string.Empty;
        [Required(ErrorMessage = "La Contraseña es obligatoria.")]
        public string Password { get; set; } = string.Empty;
    }
}