namespace PPS3.Server.Repositories.RepProducto
{
    public interface IRepProducto
    {
        Task<IEnumerable<ProductoListado>> ObtenerProductos();
        Task<Producto> ObtenerProducto(int id);
        Task<Producto> ObtenerProducto(string nombreProd);
        Task<bool> InsertarProducto(Producto producto);
        Task<bool> ActualizarProducto(Producto producto);
        Task<bool> BorrarProducto(int id);
    }
}
