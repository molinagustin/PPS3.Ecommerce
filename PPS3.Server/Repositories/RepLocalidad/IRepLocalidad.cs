namespace PPS3.Server.Repositories.RepLocalidad
{
    public interface IRepLocalidad
    {
        Task<IEnumerable<Localidad>> ObtenerLocalidades();
        Task<Localidad> ObtenerLocalidad(int id);
        Task<Localidad> ObtenerLocalidad(string nombreLocalidad);
        Task<bool> InsertarLocalidad(Localidad localidad);
        Task<bool> ActualizarLocalidad(Localidad localidad);
        Task<bool> BorrarLocalidad(int id);
    }
}
