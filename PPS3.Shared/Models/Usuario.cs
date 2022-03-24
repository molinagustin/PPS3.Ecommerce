using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Usuario
    {
        public int IdUsuarioAct { get; set; }
        [Required]
        public string NombreCompleto { get; set; } = string.Empty;
        [Required]
        public string NombreUs { get; set; } = string.Empty ;
        public string SaltCont { get; set; } = string.Empty;
        public string HashCont { get; set; } = string.Empty;
        [Required]
        public int Privilegio { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        public bool EmailVerificado { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }

        //Propiedades internas sin almacenamiento
        public string Password { get; set; } = string.Empty;
    }
}
