using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    //Si es Factura A, B, C, Nota Debito o Nota Credito
    public class TipoComprobante
    {
        public int IdTipoC { get; set; }
        [Required(ErrorMessage = "El Tipo de Comprobante debe ser especificado.")]
        public string TipoComp { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
