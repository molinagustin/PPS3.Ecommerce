namespace PPS3.Server.Repositories.RepEncabezadoRecibo
{
    public interface IRepEncabezadoRecibo
    {
        Task<IEnumerable<EncabezadoRecibo>> ObtenerRecibosPorCliente(int idCliente);
        Task<IEnumerable<Recibo>> ObtenerRecibosList();
        Task<IEnumerable<Recibo>> ObtenerRecibosListPorCliente(int idCliente);
        Task<EncabezadoRecibo> ObtenerRecibo(int idRecibo);
        Task<int> InsertarRecibo(EncabezadoRecibo encRecibo);
        Task<int> ObtenerUltimoIDRecibo(int idCliente);
    }
}
