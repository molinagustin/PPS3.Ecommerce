using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class TipoDocumento
    {
        public int IdTipoDoc { get; set; }
        [Required(ErrorMessage = "Se debe introducir una Sigla para el Tipo de Documento.")]
        public string Sigla { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe introducir una Descripcion para el Tipo de Documento.")]
        public string DescripcionTD { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
