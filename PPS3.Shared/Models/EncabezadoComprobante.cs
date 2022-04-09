using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class EncabezadoComprobante
    {
        public int IdEncab { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public string NumComp { get; set; } = string.Empty;
        public int TipoComprobante { get; set; }
        //Lo dejo como DATETIME porque si uso DATEONLY que seria la representacion de la base de datos, ocurren problemas con la serializacion y deserializacion del JSON que genera en la API
        public DateTime FechaComp { get; set; }
        public int ClienteComp { get; set; }
        public int FormaPago { get; set; }
        public int TipoVta { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ImporteFinal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal SaldoRestante { get; set; }
        public bool Pagado { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }   
    }
}
