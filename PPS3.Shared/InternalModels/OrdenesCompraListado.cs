using PPS3.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class OrdenesCompraListado
    {
        public int IdCarro { get; set; }
        public string? Estado { get; set; }
        public string? UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime? FechaOrden { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime FechaUltModif { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public bool Pagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? FormaP { get; set; }
        public string? Observaciones { get; set; }

        //Para guardar los detalles del carro
        public ICollection<DetalleCarroCompra>? DetallesCarro { get; set; }
    }
}