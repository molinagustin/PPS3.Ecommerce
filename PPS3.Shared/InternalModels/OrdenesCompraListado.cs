﻿using PPS3.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class OrdenesCompraListado
    {
        public int IdCarro { get; set; }
        public string? Estado { get; set; }
        public string? UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime? FechaOrden { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime FechaUltModif { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public bool Pagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? FormaP { get; set; }
        public string? Observaciones { get; set; }

        //Propiedad para buscar el cliente y relacionarlo en la generacion de comprobantes
        public int Cliente { get; set; }
        public bool CompGenerado { get; set; } = false;

        //Para guardar los detalles del carro
        public ICollection<DetalleCarroCompra>? DetallesCarro { get; set; }

        //Para enviar email cliente
        public bool EmailVerificado { get; set; } = false;
        public string Email { get; set; } = string.Empty;
        public string UrlString { get; set; } = string.Empty;
    }
}