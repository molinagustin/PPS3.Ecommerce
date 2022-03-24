namespace PPS3.Server.Repositories.RepProvincia
{
    public interface IRepProvincia
    {
        Task<IEnumerable<Provincia>> ObtenerProvincias();
        Task<Provincia> ObtenerProvincia(int id);
        Task<Provincia> ObtenerProvincia(string nombreProvincia);
    }
}
