namespace PPS3.Shared.InternalModels
{
    public class UnidadesMedidaListado
    {
        public int IdUnidad { get; set; }
        public string? DescripcionUnidad { get; set; }
        public bool Activo { get; set; }
        public string? UsuarioCrea { get; set; }
        public string? UsuarioModif { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}