namespace PPS3.Server.Repositories.RepMovimientoCarroCompra
{
    public interface IRepMovCarro
    {
        Task<bool> CrearMovimiento(MovimientoCarroCompra movimiento);
        Task<IEnumerable<HistorialMovimientoCarro>> ObtenerHistorial(int idOrden);
    }
}
