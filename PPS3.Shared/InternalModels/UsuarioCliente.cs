using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.InternalModels
{
    public class UsuarioCliente
    {
        //=======>>>    PROPIEDADES DE USUARIO    <<<=======
        [Required(ErrorMessage = "Se debe introducir el Nombre Completo del Usuario/Cliente a crear.")]
        public string NombreCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe introducir un Nombre de Usuario.")]
        public string NombreUs { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Se debe introducir un Email.")]
        [EmailAddress(ErrorMessage = "El Email introducido no es valido.")]
        public string Email { get; set; } = string.Empty;

        //Propiedades internas sin almacenamiento
        public string Password { get; set; } = string.Empty;

        //=======>>>    PROPIEDADES DE CLIENTE    <<<=======
        [Required(ErrorMessage = "Se debe seleccionar un Tipo de Cliente.")]
        public char TipoCliente { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar un Genero.")]
        public char Genero { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar un Tipo de Documento.")]
        public int TipoDocumento { get; set; }
        [Required(ErrorMessage = "Se debe introducir el Numero de Documento.")]
        public string NumDocumento { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe seleccionar la Condicion IVA del Cliente.")]
        public int CondIva { get; set; }
        [Required(ErrorMessage = "Se debe completar el Domicilio.")]
        public string DomicilioC { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe seleccionar una Localidad.")]
        public int LocalidadC { get; set; }
    }
}
