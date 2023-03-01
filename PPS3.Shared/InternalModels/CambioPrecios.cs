using System.ComponentModel.DataAnnotations.Schema;

namespace PPS3.Shared.InternalModels
{
    public class CambioPrecios
    {
        public bool Global { get; set; } = false;
        public bool Rubros { get; set; } = false;
        public bool TiposProds { get; set; } = false;
        public bool Provs { get; set; } = false;
        public int Rubro { get; set; } = 0;
        public int TipoProd { get; set; } = 0;
        public int Prov { get; set; } = 0;
        public bool Aumentar { get; set; } = true;
        [Column(TypeName = "decimal(10,2)")]
        public decimal Porcentaje { get; set; } = 0;
        public int IdUsAct { get; set; } = 0;
    }
}