using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    //Si es tarjeta Debito o Credito
    public class TipoTarjeta
    {
        public int IdTipoTarj { get; set; }
        [Required(ErrorMessage = "La Descripcion del Tipo de Tarjeta es obligatoria.")]
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
