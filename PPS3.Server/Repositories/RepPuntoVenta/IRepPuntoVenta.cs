namespace PPS3.Server.Repositories.RepPuntoVenta
{
    public interface IRepPuntoVenta
    {
        Task<IEnumerable<PuntoVenta>> ObtenerPuntosVentas();
        Task<PuntoVenta> ObtenerPuntoVenta(int id);
    }
}
