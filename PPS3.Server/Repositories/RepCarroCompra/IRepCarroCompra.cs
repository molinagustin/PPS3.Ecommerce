namespace PPS3.Server.Repositories.RepCarroCompra
{
    public interface IRepCarroCompra
    {
        Task<IEnumerable<CarroCompra>> ObtenerCarrosCompras();
        Task<IEnumerable<OrdenesCompraListado>> ObtenerOrdenesCompra();
        Task<OrdenesCompraListado> ObtenerOCDetalle(int NumOrden);
        Task<IEnumerable<DetalleCarroCompra>> ObtenerOCDetalles(int NumOrden);
        Task<CarroCompra> ObtenerCarroCompra(int id);
        Task<CarroCompra> ObtenerCarroActivoUsuario(int idUsuario);
        Task<int> ObtenerIdCarroNuevo(int idUsuario);
        Task<int> InsertarCarroCompra(int idUsuario);
        Task<bool> ActualizarCarroCompra(CarroCompra carroCompra);        
    }
}
