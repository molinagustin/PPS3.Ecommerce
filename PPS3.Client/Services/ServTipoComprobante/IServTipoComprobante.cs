namespace PPS3.Client.Services.ServTipoComprobante
{
    public interface IServTipoComprobante
    {
        Task<IEnumerable<TipoComprobante>> ObtenerTiposComp();
        Task<TipoComprobante> ObtenerTipoComp(int id);
    }
}
