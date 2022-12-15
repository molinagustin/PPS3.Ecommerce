using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class StockProducto
    {
        public int IdProducto { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal StockExistencia { get; set; }

        public StockProducto(){}
        public StockProducto(int id, decimal stock)
        {
            IdProducto = id;
            StockExistencia = stock;
        }
        
    }
}