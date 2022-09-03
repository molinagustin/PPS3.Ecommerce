namespace PPS3.Shared.InternalModels
{
    public class ListaCliente
    {
        public int IdCliente { get; set; }
        public char TipoCliente { get; set; }
        public char Genero { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumDocumento { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string CondIva { get; set; } = string.Empty;
        public string DomicilioC { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string LocalidadC { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
