namespace PPS3.Client.Services.ServEncabezadoPresupuesto
{
    public interface IServEncabezadoPresupuesto
    {
        Task<IEnumerable<EncabezadoPresupuesto>> ObtenerEncabezados();
        Task<IEnumerable<Presupuesto>> ObtenerPresupuestosList();
        Task<IEnumerable<DetallePresupuesto>> ObtenerDetallesPresupuestosList();
        Task<EncabezadoPresupuesto> ObtenerEncabezado(int id);
        Task<int> CrearEncabezado(EncabezadoPresupuesto encabPres);
    }
}
