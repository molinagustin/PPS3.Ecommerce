namespace PPS3.Server.Repositories.RepDetalleRecibo
{
    public interface IRepDetalleRecibo
    {
        Task<IEnumerable<DetalleRecibo>> ObtenerDetallesRecibo(int idRecibo);
        Task<IEnumerable<DetalleRecibo>> ObtenerDetallesPorComprobante(int idComprobante);
        Task<bool> InsertarDetalle(DetalleRecibo detalleRec);
    }
}
