using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.InternalModels
{
    public class ListaLocalidad
    {
        public int IdLocalidad { get; set; }
        public string NombreLoc { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string CP { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
