using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class EncabezadoComprobante
    {
        public int IdEncab { get; set; }
        [Required(ErrorMessage = "El Comprobante debe poseer un Numero que lo identifique.")]
        public string NumComprobante { get; set; } = string.Empty;
        public string CAE { get; set; } = string.Empty;
        public int TipoComprobante { get; set; }
        //Lo dejo como DATETIME porque si uso DATEONLY que seria la representacion de la base de datos, ocurren problemas con la serializacion y deserializacion del JSON que genera en la API
        public DateTime FechaComp { get; set; }
        public int ClienteComp { get; set; }
        public int ConceptoInc { get; set; }
        public int PuntoVta { get; set; }
        public int TipoVenta { get; set; }
        public int FormaPago { get; set; }
        public int Tarjeta { get; set; }
        public string NumeroTarjDebito { get; set; } = string.Empty;
        public string NumeroTarjCredito { get; set; } = string.Empty;
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
    }
}
