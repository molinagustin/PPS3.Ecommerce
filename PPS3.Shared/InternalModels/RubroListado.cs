namespace PPS3.Shared.InternalModels
{
    public class RubroListado
    {
        public int IdRubro { get; set; }
        public string? DescRubro { get; set; }
        public bool Activo { get; set; }
        public string? UsuarioCrea { get; set; }
        public string? UsuarioModif { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
