using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class EncabezadoCuentaCorriente
    {
        public int NumCC { get; set; }
        public int ClienteCC { get; set; }
        [Column(TypeName = "decimal(11,2)")]
        public decimal SaldoCCC { get; set; }
        [Required(ErrorMessage = "Se debe asignar un limite de saldo")]
        [RegularExpression(@"^\d+(?:[,]\d{0,2})?$", ErrorMessage = "El limite de saldo no es correcto")]
        [Column(TypeName = "decimal(11,2)")]
        public decimal LimiteSaldo { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
