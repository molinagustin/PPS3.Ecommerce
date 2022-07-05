using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class CuentasCorrientesListado
    {
        public int NumCC { get; set; }
        public int ClienteCC { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string NumDocumento { get; set; } = string.Empty;
        [Column(TypeName = "decimal(11,2)")]
        public decimal SaldoCCC { get; set; }
        [Column(TypeName = "decimal(11,2)")]
        public decimal LimiteSaldo { get; set; }
        public bool Activo { get; set; }

        //Para los recibos
        public ICollection<Comprobante>? Comprobantes { get; set; }
    }
}
