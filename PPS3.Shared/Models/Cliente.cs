using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "El Tipo de Cliente debe ser completado.")]
        public char TipoCliente { get; set; }
        [Required(ErrorMessage = "El Genero del Cliente debe ser seleccionado.")]
        public char Genero { get; set; }
        [Required(ErrorMessage = "El Tipo de Documento del Cliente ha de ser seleccionado.")]
        public int TipoDocumento { get; set; }
        [Required(ErrorMessage = "Se debe introducir el Numero de Documento del Cliente.")]
        public string NumDocumento { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe cargar el Nombre Completo del Cliente.")]
        public string NombreCompleto { get; set; } = string.Empty ;
        [Required(ErrorMessage = "Se debe seleccionar una Condicion de IVA del cliente.")]
        public int CondIva { get; set; }
        [Required(ErrorMessage = "Se debe cargar el Domicilio del Cliente.")]
        public string DomicilioC { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe seleccionar una Localidad para el Cliente.")]
        public int LocalidadC { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }

        //Propiedad para su cuenta corriente activa
        public bool CCActiva { get; set; }
    }
}
