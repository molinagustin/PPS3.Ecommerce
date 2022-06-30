using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class DetalleComprobante
    {
        public int IdCuerpo { get; set; }
        public int IdEncab { get; set; }
        public int ProductoCuerpo { get; set; }
        public string NombreProd { get; set; } = string.Empty;
        public string DescripcionUnidad { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal CantidadProdC { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioUnitario { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Bonificacion { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal BonificacionTotal { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
    }
}
