namespace PPS3.Server.Repositories.RepTipoTarjeta
{
    public interface IRepTipoTarjeta
    {
        Task<IEnumerable<TipoTarjeta>> ObtenerTiposTarjetas();
        Task<TipoTarjeta> ObtenerTipoTarjeta(int id);
    }
}
