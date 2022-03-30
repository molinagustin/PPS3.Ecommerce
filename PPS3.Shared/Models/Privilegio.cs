namespace PPS3.Shared.Models
{
    public class Privilegio
    {
        public int IdPrivi { get; set; }
        public string DescPrivilegio { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}