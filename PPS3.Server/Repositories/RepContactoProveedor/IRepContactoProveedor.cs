namespace PPS3.Server.Repositories.RepContactoProveedor
{
    public interface IRepContactoProveedor
    {
        Task<IEnumerable<ContactoProveedor>> ObtenerTodosContactos();
        Task<IEnumerable<ContactoProvListado>> ObtenerContactosListado();
        Task<ContactoProveedor> ObtenerContactoProveedor(int id);
        Task<ContactoProveedor> ObtenerContactoProveedor(string nombreContacto);
        Task<bool> InsertarContactoProveedor(ContactoProveedor contactoProveedor);
        Task<bool> ActualizarContactoProveedor(ContactoProveedor contactoProveedor);
        Task<bool> BorrarContactoProveedor(int id);
    }
}
