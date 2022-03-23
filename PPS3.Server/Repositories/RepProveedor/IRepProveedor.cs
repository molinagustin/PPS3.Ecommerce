namespace PPS3.Server.Repositories.RepProveedor
{
    public interface IRepProveedor
    {
        Task<IEnumerable<Proveedor>> ObtenerProveedores();
        Task<Proveedor> ObtenerProveedor(int id);
        Task<bool> InsertarProveedor(Proveedor proveedor);
        Task<bool> ActualizarProveedor(Proveedor proveedor);
        Task<bool> BorrarProveedor(int id);
    }
}
