namespace PPS3.Shared.InternalModels
{
    public class Presupuesto
    {
        public int NumPresu { get; set; }
        public string? Observaciones { get; set; }
        public string? UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public ICollection<DetallePresupuesto>? DetallePresupuesto { get; set; }
    }
}