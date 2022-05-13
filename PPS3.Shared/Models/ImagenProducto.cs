using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class ImagenProducto
    {
        public int IdImg { get; set; }
        public int IdProducto { get; set; }
        [Required(ErrorMessage = "La Imagen debe tener contenido.")]
        public byte[]? Contenido { get; set; }
        public bool Principal { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }

        //Propiedad fuera de base de datos
        public string UrlImagen { get; set; } = string.Empty;
    }
}