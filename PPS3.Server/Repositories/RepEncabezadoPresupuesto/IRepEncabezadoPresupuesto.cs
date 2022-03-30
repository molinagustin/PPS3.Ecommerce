namespace PPS3.Server.Repositories.RepEncabezadoPresupuesto
{
    public interface IRepEncabezadoPresupuesto
    {
        Task<IEnumerable<EncabezadoPresupuesto>> ObtenerTodosPresupuestos();
        Task<EncabezadoPresupuesto> ObtenerPresupuesto(int numPres);
        Task<bool> InsertarPresupuesto(EncabezadoPresupuesto encabezadoPresupuesto);
    }
}