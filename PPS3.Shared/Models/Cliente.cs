using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        [Required]
        public int TipoCliente { get; set; }
        [Required]
        public int Genero { get; set; }
        [Required]
        public int TipoDocumento { get; set; }
        [Required]
        public string NumDocumento { get; set; } = string.Empty;
        [Required]
        public string NombreCompleto { get; set; } = string.Empty ;
        [Required]
        public int CondIva { get; set; }
        [Required]
        public string DomicilioC { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        [Required]
        public int LocalidadC { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
