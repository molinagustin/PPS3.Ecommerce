namespace PPS3.Server.Repositories.RepTipoProducto
{
    public interface IRepTipoProducto
    {
        Task<IEnumerable<TipoProducto>> ObtenerTiposProductos();
        Task<TipoProducto> ObtenerTipoProducto(int id);
        Task<bool> InsertarTipoProducto(TipoProducto tipoProducto);
        Task<bool> ActualizarTipoProducto(TipoProducto tipoProducto);
        Task<bool> BorrarTipoProducto(int id);
    }
}
