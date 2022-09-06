namespace PPS3.Client.Services.ServMovimientoCarroCompra
{
    public interface IServMovCarro
    {
        Task<bool> CrearMovimiento(MovimientoCarroCompra movimiento);
        Task<IEnumerable<HistorialMovimientoCarro>> ObtenerHistorial(int idOrden);
    }
}
