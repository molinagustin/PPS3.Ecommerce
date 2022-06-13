namespace PPS3.Client.Services.ServProducto
{
    public interface IServProducto
    {
        Task<IEnumerable<ProductoListado>> ObtenerProductos();
        Task<Producto> ObtenerProducto(int id);
        Task<Producto> ObtenerProducto(string nombreProd);
        Task<bool> GuardarProducto(Producto producto);
        Task<bool> BorrarProducto(int id);
    }
}
