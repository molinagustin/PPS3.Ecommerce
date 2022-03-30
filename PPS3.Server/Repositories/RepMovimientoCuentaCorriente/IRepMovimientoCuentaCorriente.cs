namespace PPS3.Server.Repositories.RepMovimientoCuentaCorriente
{
    public interface IRepMovimientoCuentaCorriente
    {
        Task<IEnumerable<MovimientoCuentaCorriente>> ObtenerMovimientos();
        Task<MovimientoCuentaCorriente> ObtenerMovimiento(int id);
        Task<int> InsertarMovimiento(MovimientoCuentaCorriente movimientoCC);
    }
}
