namespace PPS3.Server.Repositories.RepEncabezadoPresupuesto
{
    public interface IRepEncabezadoPresupuesto
    {
        Task<IEnumerable<EncabezadoPresupuesto>> ObtenerTodosPresupuestos();
        Task<IEnumerable<Presupuesto>> ObtenerPresupuestosList();
        Task<IEnumerable<DetallePresupuesto>> ObtenerDetallesPresupuestosList();
        Task<EncabezadoPresupuesto> ObtenerPresupuesto(int numPres);
        Task<int> InsertarPresupuesto(EncabezadoPresupuesto encabezadoPresupuesto);
    }
}