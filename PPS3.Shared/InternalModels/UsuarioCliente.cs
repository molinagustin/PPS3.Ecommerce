﻿using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.InternalModels
{
    public class UsuarioCliente
    {
        //=======>>>    PROPIEDADES DE USUARIO    <<<=======
        public int IdUsuarioAct { get; set; }
        [Required(ErrorMessage = "Se debe introducir el Nombre Completo del Usuario/Cliente a crear.")]
        [RegularExpression(@"^[a-zA-Z ]{4,}$", ErrorMessage = "El nombre debe contener letras mayúsculas y/o minúsculas solamente y se puede usar espaciado (Min. 4)")]
        public string NombreCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe introducir un Nombre de Usuario.")]
        [RegularExpression(@"^[a-zA-Z]{4,8}$", ErrorMessage = "El nombre de usuario debe contener letras mayúsculas y/o minúsculas solamente (Min. 4 Max. 8)")]
        public string NombreUs { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Se debe introducir un Email.")]
        [EmailAddress(ErrorMessage = "El Email introducido no es valido.")]
        public string Email { get; set; } = string.Empty;       

        //=======>>>    PROPIEDADES DE CLIENTE    <<<=======
        [Required(ErrorMessage = "Se debe seleccionar un Tipo de Cliente.")]
        public char TipoCliente { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar un Género.")]
        public char Genero { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar un Tipo de Documento.")]
        public int TipoDocumento { get; set; }
        [Required(ErrorMessage = "Se debe introducir el Número de Documento.")]
        [RegularExpression(@"^[0-9]{8,11}$", ErrorMessage = "El número de documento debe ser completo.")]
        public string NumDocumento { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe seleccionar la Condición IVA del Cliente.")]
        public int CondIva { get; set; }
        [Required(ErrorMessage = "Se debe completar el Domicilio.")]
        public string DomicilioC { get; set; } = string.Empty;
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Solo se aceptan números sin espacio.")]
        public string Telefono { get; set; } = string.Empty;
        [Required(ErrorMessage = "Se debe seleccionar una Localidad.")]
        public int LocalidadC { get; set; }

        //=======>>>    CAMBIO DE PASSWORD    <<<=======
        //Propiedades internas sin almacenamiento
        public string SaltCont { get; set; } = string.Empty;
        public string HashCont { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña no puede estar vacía.")]
        [RegularExpression(@"^.{4,}$", ErrorMessage = "La contraseña debe ser de Minimo 4 caracteres")]
        public string Password { get; set; } = string.Empty;
        [RegularExpression(@"^.{4,}$", ErrorMessage = "La contraseña debe ser de Minimo 4 caracteres")]
        public string CambioPassword { get; set; } = string.Empty;

        //=======>>>    Envio Email Confirmacion    <<<=======
        public string URL { get; set; } = string.Empty;
    }
}
