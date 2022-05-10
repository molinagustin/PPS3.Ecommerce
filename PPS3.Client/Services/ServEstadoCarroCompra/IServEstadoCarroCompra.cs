namespace PPS3.Client.Services.ServEstadoCarroCompra
{
    public interface IServEstadoCarroCompra
    {
        Task<IEnumerable<EstadoCarroCompra>> ObtenerEstados();
        Task<EstadoCarroCompra> ObtenerEstado(int id);
    }
}
