using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS3.Shared.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProd { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Rubro { get; set; }
        public int TipoProd { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PrecioCosto { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PrecioFinal { get; set; }
        public int Proveedor { get; set; }
        public int UnidadMedida { get; set; }
        [Column(TypeName = "decimal(11,2)")]
        public decimal CantMinAlerta { get; set; }
        public bool Stockeable { get; set; }
        public bool Activo { get; set; }
        public int UsuarioCrea { get; set; }
        public DateTime FechaCrea { get; set; }
        public int UsuarioModif { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}
