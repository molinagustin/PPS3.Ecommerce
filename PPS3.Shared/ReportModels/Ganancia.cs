using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.ReportModels
{
    public class Ganancia
    {
        public DateTime FechaRecibo { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalDia { get; set; }
    }
}