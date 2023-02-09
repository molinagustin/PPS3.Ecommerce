using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.ReportModels
{
    public class StockProd
    {
        public string NombreProd { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10,2)")]
        public decimal StockExistencia { get; set; }
        public string DescripcionUnidad { get; set; } = string.Empty;
    }
}