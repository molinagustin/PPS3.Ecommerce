namespace PPS3.Client.Services.ServLocalidad
{
    public interface IServLocalidad
    {
        Task<IEnumerable<Localidad>> ObtenerLocalidades();
        Task<IEnumerable<ListaLocalidad>> ObtenerListaLocalidades();
        Task<Localidad> ObtenerLocalidad(int id);
        Task<Localidad> ObtenerLocalidad(string nombreLocalidad);
        Task<bool> GuardarLocalidad(Localidad localidad);
        Task<bool> BorrarLocalidad(int id);
    }
}
