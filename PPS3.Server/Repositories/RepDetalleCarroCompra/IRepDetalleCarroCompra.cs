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
        Task<bool> ActualizarStockProductos(int id); //Sirve para cuando se da de baja el comprobante y hay que volver a sumar la cantidad de los productos a su stock
    }
}