using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS3.Shared.Models
{
    public class Usuario
    {
        public int IdUsuarioAct { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreUs { get; set; } = string.Empty ;
        public string SaltCont { get; set; } = string.Empty;
        public string HashCont { get; set; } = string.Empty;
        public int Privilegio { get; set; }
        public int IdCliente { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool EmailVerificado { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
