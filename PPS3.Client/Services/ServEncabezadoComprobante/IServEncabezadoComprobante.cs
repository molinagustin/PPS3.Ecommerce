using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServEncabezadoComprobante
{
    public interface IServEncabezadoComprobante
    {
        Task<IEnumerable<EncabezadoComprobante>> ObtenerEncabezados();
        Task<IEnumerable<Comprobante>> ObtenerComprobantesListCliente(int idCliente);
        Task<IEnumerable<DetalleComprobante>> ObtenerDetallesComprobantesList();
        Task<EncabezadoComprobante> ObtenerEncabezado(int id);
        Task<int> CrearEncabezado(EncabezadoComprobante encabezadoComp);
    }
}
