using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class ProductoListado
    {
        public int IdProducto { get; set; }
        public string? NombreProd { get; set; }
        public string? Descripcion { get; set; }
        public string? DescRubro { get; set; }
        public string? DescripcionTipo { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PrecioCosto { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal PrecioFinal { get; set; }
        public string? NombreProv { get; set; }
        public string? DescripcionUnidad { get; set; }
        [Column(TypeName = "decimal(11,2)")]
        public decimal CantMinAlerta { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal StockExistencia { get; set; }
        public string? UsuarioCrea { get; set; }
        public string? UsuarioModif { get; set; }
        public DateTime FechaCrea { get; set; }
        public DateTime FechaUltModif { get; set; }
    }
}