namespace PPS3.Shared.Models
{
    public class FormaPago
    {
        public int IdFormaP { get; set; }
        public string FormaP { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
