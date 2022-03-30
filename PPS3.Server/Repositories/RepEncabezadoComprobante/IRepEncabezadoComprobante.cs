namespace PPS3.Server.Repositories.RepEncabezadoComprobante
{
    public interface IRepEncabezadoComprobante
    {
        Task<IEnumerable<EncabezadoComprobante>> ObtenerEncabezadosComp();
        Task<EncabezadoComprobante> ObtenerEncabezadoComp(int id);
        Task<int> InsertarEncabezadoComp(EncabezadoComprobante encabezadoComp);
    }
}
