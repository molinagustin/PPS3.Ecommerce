namespace PPS3.Server.Repositories.RepTarjeta
{
    public interface IRepTarjeta
    {
        Task<IEnumerable<Tarjeta>> ObtenerTarjetas();
        Task<IEnumerable<ListaTarjeta>> ObtenerListaTarjetas();
        Task<Tarjeta> ObtenerTarjeta(int id);
    }
}
