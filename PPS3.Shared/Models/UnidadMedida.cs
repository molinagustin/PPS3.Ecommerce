using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class UnidadMedida
    {
        public int IdUnidad { get; set; }
        [Required(ErrorMessage = "El nombre de la Unidad de Medida es obligatorio.")]
        public string DescripcionUnidad { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
