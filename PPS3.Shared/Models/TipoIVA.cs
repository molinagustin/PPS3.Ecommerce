using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class TipoIVA
    {
        public int IdTipoIva { get; set; }
        [Required(ErrorMessage = "La Descripcion es obligatoria.")]
        public string Descripcion { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Porcentaje debe ser completado.")]
        [Column(TypeName = "decimal(11,2)")]
        public decimal Porcentaje { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
