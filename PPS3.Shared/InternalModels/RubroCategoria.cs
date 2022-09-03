namespace PPS3.Shared.InternalModels
{
    public class RubroCategoria
    {
        public int IdRubro { get; set; }
        public string DescRubro { get; set; } = string.Empty;
        public ICollection<TipoProductoCategoria>? Categorias { get; set; }
    }
}
