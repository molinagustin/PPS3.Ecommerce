namespace PPS3.Server.Repositories.RepCliente
{
    public interface IRepCliente
    {
        Task<IEnumerable<Cliente>> ObtenerClientes();
        Task<Cliente> ObtenerCliente(int id);
        Task<Cliente> ObtenerCliente(string nombreCliente);
        Task<int> ObtenerIdCliente(string nombreCliente, string numDocumento);
        Task<int> CrearCliente(UsuarioCliente usuarioCliente);
        Task<bool> ActualizarCliente(Cliente cliente);
        Task<bool> BorrarCliente(int id);
    }
}
