namespace PPS3.Shared.InternalModels
{
    public class ProveedorListado
    {
        public int IdProveedor { get; set; }
        public string? NombreProv { get; set; }
        public string? DomicilioProv { get; set; }
        public bool Activo { get; set; }
        public string? UsuarioCrea { get; set; }
        public string? UsuarioModif { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}