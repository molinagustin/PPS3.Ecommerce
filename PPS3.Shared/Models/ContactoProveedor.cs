using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class ContactoProveedor
    {
        public int IdProvCont { get; set; }
        [Required(ErrorMessage = "El Nombre del Contacto de Proveedor es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty ;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe seleccionar el Proveedor del Contacto.")]
        public int Proveedor { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
