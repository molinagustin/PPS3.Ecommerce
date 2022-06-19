using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Usuario
    {
        public int IdUsuarioAct { get; set; }
        [Required(ErrorMessage = "El Nombre Completo debe ser introducido.")]
        [RegularExpression(@"^[a-zA-Z ]{4,}$", ErrorMessage = "El nombre de usuario debe contener letras mayusculas y/o minusculas solamente y se puede usar espaciado (Min. 4)")]
        public string NombreCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z]{4,8}$", ErrorMessage = "El nombre de usuario debe contener letras mayusculas y/o minusculas solamente (Min. 4 Max. 8)")]
        public string NombreUs { get; set; } = string.Empty ; 
        public string SaltCont { get; set; } = string.Empty;
        public string HashCont { get; set; } = string.Empty;
        public int Privilegio { get; set; }
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "El Email es un campo obligatorio.")]
        [EmailAddress(ErrorMessage = "El Email introducido no es valido.")]
        public string Email { get; set; } = string.Empty;
        public bool EmailVerificado { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }

        //Propiedades internas sin almacenamiento
        //[Required(ErrorMessage = "La contraseña es un campo obligatorio.")]
        public string Password { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
