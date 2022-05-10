namespace PPS3.Client.Services.ServEncabezadoPresupuesto
{
    public interface IServEncabezadoPresupuesto
    {
        Task<IEnumerable<EncabezadoPresupuesto>> ObtenerEncabezados();
        Task<EncabezadoPresupuesto> ObtenerEncabezado(int id);
        Task<bool> CrearEncabezado(EncabezadoPresupuesto encabPres);
    }
}
