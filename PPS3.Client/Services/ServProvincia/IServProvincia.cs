namespace PPS3.Client.Services.ServProvincia
{
    public interface IServProvincia
    {
        Task<IEnumerable<Provincia>> ObtenerProvincias();
        Task<Provincia> ObtenerProvincia(int id);
        Task<Provincia> ObtenerProvincia(string nombreProv);
    }
}
