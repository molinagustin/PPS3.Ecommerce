using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.InternalModels
{
    public class Presupuesto
    {
        public int NumPresu { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        public string? UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public ICollection<DetallePresupuesto>? DetallePresupuesto { get; set; }
    }
}