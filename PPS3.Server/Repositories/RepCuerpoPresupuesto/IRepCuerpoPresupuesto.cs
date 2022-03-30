namespace PPS3.Server.Repositories.RepCuerpoPresupuesto
{
    public interface IRepCuerpoPresupuesto
    {
        Task<IEnumerable<CuerpoPresupuesto>> ObtenerCuerposDePresupuesto(int numPresupuesto);
        Task<CuerpoPresupuesto> ObtenerCuerpoPresupuesto(int idCuerpo);
        Task<bool> InsertarNuevoCuerpo(CuerpoPresupuesto cuerpoPresupuesto);
    }
}
