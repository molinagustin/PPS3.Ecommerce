﻿using System.ComponentModel.DataAnnotations;

namespace PPS3.Shared.Models
{
    public class Usuario
    {
        public int IdUsuarioAct { get; set; }
        [Required(ErrorMessage = "El Nombre Completo debe ser introducido.")]
        public string NombreCompleto { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio.")]
        public string NombreUs { get; set; } = string.Empty ;
        public string SaltCont { get; set; } = string.Empty;
        public string HashCont { get; set; } = string.Empty;
        public int Privilegio { get; set; }
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "El Email es un campo obligatorio.")]
        [EmailAddress(ErrorMessage = "El Email introducido no es valido.")]
        public string Email { get; set; } = string.Empty;
        public bool EmailVerificado { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }

        //Propiedades internas sin almacenamiento
        public string Password { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
