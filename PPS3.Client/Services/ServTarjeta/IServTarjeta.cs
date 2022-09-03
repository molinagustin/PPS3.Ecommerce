namespace PPS3.Client.Services.ServTarjeta
{
    public interface IServTarjeta
    {
        Task<IEnumerable<Tarjeta>> ObtenerTarjetas();
        Task<IEnumerable<ListaTarjeta>> ObtenerListaTarjetas();
        Task<Tarjeta> ObtenerTarjeta(int id);
    }
}
