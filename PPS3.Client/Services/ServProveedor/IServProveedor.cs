namespace PPS3.Client.Services.ServProveedor
{
    public interface IServProveedor
    {
        Task<IEnumerable<Proveedor>> ObtenerProveedores();
        Task<IEnumerable<ProveedorListado>> ObtenerProveedoresListado();
        Task<Proveedor> ObtenerProveedor(int id);
        Task<int> GuardarProveedor(Proveedor proveedor);
        Task<bool> BorrarProveedor(int id);
    }
}
