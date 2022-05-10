namespace PPS3.Client.Services.ServTipoVenta
{
    public interface IServTipoVenta
    {
        Task<IEnumerable<TipoVenta>> ObtenerTiposVentas();
        Task<TipoVenta> ObtenerTipoVenta(int id);
    }
}
