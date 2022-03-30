namespace PPS3.Server.Repositories.RepEstadoCarroCompra
{
    public interface IRepEstadoCarroCompra
    {
        Task<IEnumerable<EstadoCarroCompra>> ObtenerEstados();
        Task<EstadoCarroCompra> ObtenerEstado(int id);
    }
}
