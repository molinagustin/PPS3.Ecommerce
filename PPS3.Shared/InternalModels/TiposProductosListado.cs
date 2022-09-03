namespace PPS3.Shared.InternalModels
{
    public class TiposProductosListado
    {
        public int IdTipo { get; set; }
        public string? DescripcionTipo { get; set; }
        public string? Rubro { get; set; }
        public bool Activo { get; set; }
        public string? UsuarioCrea { get; set; }
        public string? UsuarioModif { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
