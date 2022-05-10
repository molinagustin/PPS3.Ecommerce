namespace PPS3.Client.Services.ServProveedor
{
    public interface IServProveedor
    {
        Task<IEnumerable<Proveedor>> ObtenerProveedores();
        Task<Proveedor> ObtenerProveedor(int id);
        Task<bool> GuardarProveedor(Proveedor proveedor);
        Task<bool> BorrarProveedor(int id);
    }
}
