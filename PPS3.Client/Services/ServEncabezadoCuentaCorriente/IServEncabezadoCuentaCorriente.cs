using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServEncabezadoCuentaCorriente
{
    public interface IServEncabezadoCuentaCorriente
    {
        Task<IEnumerable<EncabezadoCuentaCorriente>> ObtenerCuentasCorrientes();
        Task<IEnumerable<CuentasCorrientesListado>> ObtenerCCListado();
        Task<IEnumerable<CuentasCorrientesListado>> ObtenerListadoCCCompr();
        Task<EncabezadoCuentaCorriente> ObtenerCuentaCorriente(int numCC);
        Task<EncabezadoCuentaCorriente> ObtenerCCCliente(int idCliente);
        Task<bool> GuardarEncabCC(EncabezadoCuentaCorriente encabezadoCC);
        Task<bool> BorrarEncabCC(int id);
    }
}
