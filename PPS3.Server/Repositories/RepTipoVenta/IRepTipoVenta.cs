namespace PPS3.Server.Repositories.RepTipoVenta
{
    public interface IRepTipoVenta
    {
        Task<IEnumerable<TipoVenta>> ObtenerTiposVentas();
        Task<TipoVenta> ObtenerTipoVenta(int id);
    }
}
