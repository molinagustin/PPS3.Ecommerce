using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.Models
{
    public class CarroCompra
    {
        public int IdCarro { get; set; }        
        public int Estado { get; set; } //Estado actual del carro (Activo, Pendiente, Finalizado, etc.)
        public int UsuarioCarro { get; set; }
        public DateTime FechaCrea { get; set; } //Cuando se crea el carro
        public DateTime? FechaOrden { get; set; } //Cuando se solicitan los productos del carro, paso en el cual el Estado del carro pasa de Activo a Pendiente (Se lo toma como orden ahi)
        public DateTime? FechaEntrega { get; set; }
        public DateTime FechaUltModif { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public bool Pagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public int MetodoPago { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public bool CompGenerado { get; set; }
        public int UsGenCot { get; set; }
    }
}
