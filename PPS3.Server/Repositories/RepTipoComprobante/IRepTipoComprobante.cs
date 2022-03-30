namespace PPS3.Server.Repositories.RepTipoComprobante
{
    public interface IRepTipoComprobante
    {
        Task<IEnumerable<TipoComprobante>> ObtenerTiposComprobantes();
        Task<TipoComprobante> ObtenerTipoComprobante(int id);
    }
}
