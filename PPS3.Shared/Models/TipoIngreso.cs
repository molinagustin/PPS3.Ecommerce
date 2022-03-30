using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    //Clase que indica como fue el ingreso del dinero, es decir, si fue pagado totalmente, si se hizo un ingreso a la cuenta corriente y quedo como deuda, o bien si es una entrega a la cuenta corriente de una deuda existente
    public class TipoIngreso
    {
        public int IdTipoIngreso { get; set; }
        [Required(ErrorMessage = "La Descripcion del Tipo de Ingreso es obligatoria.")]
        public string TipoIng { get; set; } = string.Empty;
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
