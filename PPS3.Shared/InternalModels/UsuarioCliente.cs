using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.InternalModels
{
    public class UsuarioCliente
    {
        //=======>>>    PROPIEDADES DE USUARIO    <<<=======
        [Required]
        public string NombreCompleto { get; set; } = string.Empty;
        [Required]
        public string NombreUs { get; set; } = string.Empty;
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;

        //Propiedades internas sin almacenamiento
        public string Password { get; set; } = string.Empty;

        //=======>>>    PROPIEDADES DE CLIENTE    <<<=======
        [Required]
        public int TipoCliente { get; set; }
        [Required]
        public int Genero { get; set; }
        [Required]
        public int TipoDocumento { get; set; }
        [Required]
        public string NumDocumento { get; set; } = string.Empty;
        [Required]
        public int CondIva { get; set; }
        [Required]
        public string DomicilioC { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        [Required]
        public int LocalidadC { get; set; }
    }
}
