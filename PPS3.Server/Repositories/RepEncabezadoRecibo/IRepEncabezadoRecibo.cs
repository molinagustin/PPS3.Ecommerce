namespace PPS3.Server.Repositories.RepEncabezadoRecibo
{
    public interface IRepEncabezadoRecibo
    {
        Task<IEnumerable<EncabezadoRecibo>> ObtenerRecibosPorCliente(int idCliente);
        Task<EncabezadoRecibo> ObtenerRecibo(int idRecibo);
        Task<int> InsertarRecibo(EncabezadoRecibo encRecibo);
        Task<int> ObtenerUltimoIDRecibo(int idCliente);
    }
}
