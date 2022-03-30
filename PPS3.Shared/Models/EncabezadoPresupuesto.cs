namespace PPS3.Shared.Models
{
    public class EncabezadoPresupuesto
    {
        public int NumPresu { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
    }
}
