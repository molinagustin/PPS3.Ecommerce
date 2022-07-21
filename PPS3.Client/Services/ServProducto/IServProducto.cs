namespace PPS3.Client.Services.ServProducto
{
    public interface IServProducto
    {
        Task<IEnumerable<ProductoListado>> ObtenerProductos();
        Task<IEnumerable<ProductoListado>> ObtenerProductosInactivos();
        Task<IEnumerable<ProductoListado>> ObtenerUltimos5Productos();
        Task<Producto> ObtenerProducto(int id);
        Task<ProductoListado> ObtenerProductoListado(int id);
        Task<Producto> ObtenerProducto(string nombreProd);
        Task<bool> GuardarProducto(Producto producto);
        Task<bool> BorrarProducto(int id, int idUsu);
        Task<int> UltimoProductoCreado(int idUsuario);
    }
}
