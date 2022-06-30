namespace PPS3.Client.Services.ServEncabezadoRecibo
{
    public interface IServEncabezadoRecibo
    {
        Task<IEnumerable<EncabezadoRecibo>> ObtenerEncabRecCliente(int idCliente);
        Task<IEnumerable<Recibo>> ObtenerRecibosListPorCliente(int idCliente);
        Task<EncabezadoRecibo> ObtenerEncab(int idRecibo);
        Task<bool> CrearEncabRec(EncabezadoRecibo encabRec);
    }
}
