using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.InternalModels
{
    public class ListaTarjeta
    {
        public int IdTarjeta { get; set; }
        public string NombreTarj { get; set; } = string.Empty;
        public string TipoTarj { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
