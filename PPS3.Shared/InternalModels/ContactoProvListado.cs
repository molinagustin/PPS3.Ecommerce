namespace PPS3.Shared.InternalModels
{
    public class ContactoProvListado
    {
        public int IdProvCont { get; set; }
        public string? Nombre { get; set; }
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? NombreProv { get; set; }
        public bool Activo { get; set; }
        public string? UsuarioCrea { get; set; }
        public string? UsuarioModif { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
