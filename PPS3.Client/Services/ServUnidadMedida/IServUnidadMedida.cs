namespace PPS3.Client.Services.ServUnidadMedida
{
    public interface IServUnidadMedida
    {
        Task<IEnumerable<UnidadMedida>> ObtenerUnidades();
        Task<UnidadMedida> ObtenerUnidad(int id);
        Task<bool> GuardarUnidad(UnidadMedida unidad);
        Task<bool> BorrarUnidad(int id);
    }
}
