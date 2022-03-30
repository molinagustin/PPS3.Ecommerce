using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        [Required(ErrorMessage = "Se debe introducir un Nombre para el Proveedor.")]
        public string NombreProv { get; set; } = string.Empty;
        public string DomicilioProv { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
