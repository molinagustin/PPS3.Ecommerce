using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServCliente
{
    public interface IServCliente
    {
        Task<IEnumerable<Cliente>> ObtenerClientes();
        Task<Cliente> ObtenerCliente(int id);
        Task<Cliente> ObtenerCliente(string nombreCliente);
    }
}
