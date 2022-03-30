namespace PPS3.Server.Repositories.RepDetalleCarroCompra
{
    public interface IRepDetalleCarroCompra
    {
        Task<IEnumerable<DetalleCarroCompra>> ObtenerTodosDetalles();
        Task<IEnumerable<DetalleCarroCompra>> ObtenerDetallesCarro(int idCarro);
        Task<DetalleCarroCompra> ObtenerDetalle(int id);
        Task<bool> InsertarDetalleCarro(DetalleCarroCompra detalleCarroCompra);
        Task<bool> ActualizarDetalleCarro(DetalleCarroCompra detalleCarroCompra);
        Task<bool> BorrarDetalleCarro(int id);
    }
}