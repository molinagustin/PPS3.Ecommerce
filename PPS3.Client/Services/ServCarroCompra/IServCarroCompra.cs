using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServCarroCompra
{
    public interface IServCarroCompra
    {
        Task<IEnumerable<CarroCompra>> ObtenerCarros();
        Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompra();
        Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompraComprobantes();
        Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompraUsuario(int idUsuario);
        Task<OrdenesCompraListado> ObtenerOCDetalle(int NumOrden);
        Task<IEnumerable<DetalleCarroCompra>> ObtenerOCDetalles(int NumOrden);
        Task<CarroCompra> ObtenerCarro(int id);
        Task<CarroCompra> ObtenerCarroActivoUsuario(int idUsuario);
        Task<bool> GuardarCarro(CarroCompra carro);
        Task<bool> BajaComprobanteCarro(int id);
        Task<IEnumerable<MovimientosPago>> ObtenerMovimientosPago(int NumOrden);
        Task<int> InsertarCarroComprasCotizacion(CarroCompra carroCompra);
        Task<IEnumerable<OrdenesCompraCotiz>> ObtenerOCCotiz();
    }
}
