using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServEncabezadoComprobante
{
    public interface IServEncabezadoComprobante
    {
        Task<IEnumerable<EncabezadoComprobante>> ObtenerEncabezados();
        Task<EncabezadoComprobante> ObtenerEncabezado(int id);
        Task<int> CrearEncabezado(EncabezadoComprobante encabezadoComp);
    }
}
