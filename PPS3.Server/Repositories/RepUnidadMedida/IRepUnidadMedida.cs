namespace PPS3.Server.Repositories.RepUnidadMedida
{
    public interface IRepUnidadMedida
    {
        Task<IEnumerable<UnidadMedida>> ObtenerUnidadesMedida();
        Task<IEnumerable<UnidadesMedidaListado>> ObtenerUnidadMedListado();
        Task<UnidadMedida> ObtenerUnidadMedida(int id);
        Task<bool> InsertarUnidadMedida(UnidadMedida unidadMedida);
        Task<bool> ActualizarUnidadMedida(UnidadMedida unidadMedida);
        Task<bool> BorrarUnidadMedida(int id);
    }
}
