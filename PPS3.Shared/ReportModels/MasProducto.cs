using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.ReportModels
{
    public class MasProducto
    {
        public string NombreProd { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; }
        public string DescripcionUnidad { get; set; } = string.Empty;
    }
}