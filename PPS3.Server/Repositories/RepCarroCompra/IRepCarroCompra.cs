namespace PPS3.Server.Repositories.RepCarroCompra
{
    public interface IRepCarroCompra
    {
        Task<IEnumerable<CarroCompra>> ObtenerCarrosCompras();
        Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompra();
        Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompraComprobantes();
        Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompraUsuario(int idUsuario);
        Task<OrdenesCompraListado> ObtenerOCDetalle(int NumOrden);
        Task<IEnumerable<DetalleCarroCompra>> ObtenerOCDetalles(int NumOrden);
        Task<IEnumerable<DetalleCarroCompra>> ObtenerOCDetallesComprobantes(List<int> carros);
        Task<CarroCompra> ObtenerCarroCompra(int id);
        Task<CarroCompra> ObtenerCarroActivoUsuario(int idUsuario);
        Task<int> ObtenerIdCarroNuevo(int idUsuario);
        Task<int> InsertarCarroCompra(int idUsuario);
        Task<bool> ActualizarCarroCompra(CarroCompra carroCompra);
        Task<bool> BajaComprobanteCarro(int id);
    }
}
