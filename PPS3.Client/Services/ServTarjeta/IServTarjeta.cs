namespace PPS3.Client.Services.ServTarjeta
{
    public interface IServTarjeta
    {
        Task<IEnumerable<Tarjeta>> ObtenerTarjetas();
        Task<Tarjeta> ObtenerTarjeta(int id);
    }
}
