using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServCuerpoPresupuesto
{
    public interface IServCuerpoPresupuesto
    {
        Task<IEnumerable<CuerpoPresupuesto>> ObtenerCuerpos(int numPresupuesto);
        Task<CuerpoPresupuesto> ObtenerCuerpo(int idCuerpo);
        Task<bool> CrearCuerpo(CuerpoPresupuesto cuerpoPresupuesto);
    }
}
