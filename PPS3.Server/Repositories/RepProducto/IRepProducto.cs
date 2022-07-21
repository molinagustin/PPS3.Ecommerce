namespace PPS3.Server.Repositories.RepProducto
{
    public interface IRepProducto
    {
        Task<IEnumerable<ProductoListado>> ObtenerProductos();
        Task<IEnumerable<ProductoListado>> ObtenerProductosInactivos();
        Task<IEnumerable<ProductoListado>> ObtenerUltimos5Productos();
        Task<Producto> ObtenerProducto(int id);
        Task<ProductoListado> ObtenerProductoListado(int id);
        Task<Producto> ObtenerProducto(string nombreProd);
        Task<bool> InsertarProducto(Producto producto);
        Task<bool> ActualizarProducto(Producto producto);
        Task<bool> BorrarProducto(int id, int idUsu);
        Task<int> UltimoProductoCreado(int idUsuario);
    }
}
