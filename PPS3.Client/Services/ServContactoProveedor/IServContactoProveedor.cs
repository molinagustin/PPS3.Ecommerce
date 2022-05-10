namespace PPS3.Client.Services.ServContactoProveedor
{
    public interface IServContactoProveedor
    {
        Task<IEnumerable<ContactoProveedor>> ObtenerContactos();
        Task<ContactoProveedor> ObtenerContacto(int id);
        Task<ContactoProveedor> ObtenerConctacto(string nombreContacto);
        Task<bool> GuardarContacto(ContactoProveedor contacto);
        Task<bool> BorrarContacto(int id);
    }
}