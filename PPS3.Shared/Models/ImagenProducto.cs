using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class ImagenProducto
    {
        public int IdImg { get; set; }
        public int IdProducto { get; set; }
        [Required(ErrorMessage = "La Imagen debe tener su ruta de acceso.")]
        public string UrlImg { get; set; } = string.Empty;
        public bool Principal { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}