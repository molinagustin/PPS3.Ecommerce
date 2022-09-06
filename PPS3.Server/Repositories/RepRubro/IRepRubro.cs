namespace PPS3.Server.Repositories.RepRubro
{
    public interface IRepRubro
    {
        Task<IEnumerable<Rubro>> ObtenerRubros();
        Task<IEnumerable<RubroListado>> ObtenerRubrosListado();
        Task<IEnumerable<RubroCategoria>> ObtenerRubrosCategorias();
        Task<IEnumerable<TipoProductoCategoria>> ObtenerTiposProductosCategorias();
        Task<Rubro> ObtenerRubro(int id);
        Task<int> InsertarRubro(Rubro rubro);
        Task<int> ActualizarRubro(Rubro rubro);
        Task<bool> BorrarRubro(int id);
    }
}
