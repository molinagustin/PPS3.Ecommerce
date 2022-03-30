namespace PPS3.Server.Repositories.RepTipoCliente
{
    public interface IRepTipoCliente
    {
        Task<IEnumerable<TipoCliente>> ObtenerTiposClientes();

        Task<TipoCliente> ObtenerTipoCliente(int id);
    }
}
