using PPS3.Shared.InternalModels;

namespace PPS3.Server.Repositories.RepHerramientas
{
    public interface IRepHerramientas
    {
        Task<int> CambiarPrecios(CambioPrecios cambios);
    }
}