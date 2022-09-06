namespace PPS3.Server.Repositories.RepProveedor
{
    public interface IRepProveedor
    {
        Task<IEnumerable<Proveedor>> ObtenerProveedores();
        Task<IEnumerable<ProveedorListado>> ObtenerProveedoresListado();
        Task<Proveedor> ObtenerProveedor(int id);
        Task<int> InsertarProveedor(Proveedor proveedor);
        Task<int> ActualizarProveedor(Proveedor proveedor);
        Task<bool> BorrarProveedor(int id);
    }
}
