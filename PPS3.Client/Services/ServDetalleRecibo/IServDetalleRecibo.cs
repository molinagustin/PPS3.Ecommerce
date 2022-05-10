using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServDetalleRecibo
{
    public interface IServDetalleRecibo
    {
        Task<IEnumerable<DetalleRecibo>> ObtenerDetallesPorComprobante(int idComprobante);
        Task<IEnumerable<DetalleRecibo>> ObtenerDetallesRecibo(int idRecibo);
        Task<bool> CrearDetalleRecibo(DetalleRecibo detalleRec);
    }
}
