using Microsoft.AspNetCore.Mvc;

namespace PPS3.Client.Services.ServCuerpoComprobante
{
    public interface IServCuerpoComprobante
    {
        Task<IEnumerable<CuerpoComprobante>> ObtenerCuerpos(int numCabeceraComp);
        Task<CuerpoComprobante> ObtenerCuerpo(int numCuerpo);
        Task<bool> CrearCuerpo(CuerpoComprobante cuerpoComp);
    }
}
