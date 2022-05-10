namespace PPS3.Client.Services.ServTipoTarjeta
{
    public interface IServTipoTarjeta
    {
        Task<IEnumerable<TipoTarjeta>> ObtenerTiposTarj();
        Task<TipoTarjeta> ObtenerTipoTarj(int id);
    }
}
