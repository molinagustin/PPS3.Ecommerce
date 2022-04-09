using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    //Esta clase hace referencia a si cuando se realiza una venta, ésta es registrada dentro de la cuenta corriente o simplemente no se registra.
    public class TipoVenta
    {
        public int IdTipoVta { get; set; }
        [Required(ErrorMessage = "La Descripcion del Tipo de Venta es obligatoria.")]
        public string TipoVta { get; set; } = string.Empty;
    }
}