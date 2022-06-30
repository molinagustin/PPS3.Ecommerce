namespace PPS3.Server.Repositories.RepEncabezadoCuentaCorriente
{
    public interface IRepEncabezadoCuentaCorriente
    {
        Task<IEnumerable<EncabezadoCuentaCorriente>> ObtenerCuentasCorrientes();
        Task<IEnumerable<CuentasCorrientesListado>> ObtenerCCListado();
        Task<EncabezadoCuentaCorriente> ObtenerCuentaCorriente(int numCC);
        Task<EncabezadoCuentaCorriente> ObtenerCCCliente(int idCliente);
        Task<bool> InsertarCuentaCorriente(EncabezadoCuentaCorriente encabezadoCC);
        Task<bool> ActualizarCuentaCorriente(EncabezadoCuentaCorriente encabezadoCC);
        Task<bool> BorrarCuentaCorriente(int numCC);
    }
}