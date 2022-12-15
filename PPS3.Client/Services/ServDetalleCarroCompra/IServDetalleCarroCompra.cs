using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServDetalleCarroCompra
{
    public interface IServDetalleCarroCompra
    {
        Task<IEnumerable<DetalleCarroCompra>> ObtenerTodosDetalles();
        Task<DetalleCarroCompra> ObtenerDetalle(int id);
        Task<IEnumerable<DetalleCarroCompra>> ObtenerDetallesCarro(int idCarro);
        Task<bool> GuardarDetalle(DetalleCarroCompra detalle);
        Task<bool> BorrarDetalle(int id);
        Task<bool> ActualizarStockProductos(int id);
    }
}
