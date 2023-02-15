using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class CondicionIVA
    {
        public int IdCondicion { get; set; }
        [Required(ErrorMessage = "Se debe cargar una Descripción para la Condición del IVA.")]
        public string DescripcionCond { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}