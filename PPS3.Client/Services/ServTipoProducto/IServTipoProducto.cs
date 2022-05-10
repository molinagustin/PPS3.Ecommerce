namespace PPS3.Client.Services.ServTipoProducto
{
    public interface IServTipoProducto
    {
        Task<IEnumerable<TipoProducto>> ObtenerTiposProd();
        Task<TipoProducto> ObtenerTipoProd(int id);
        Task<bool> GuardarTipoProd(TipoProducto tipoProd);
        Task<bool> BorrarTipoProd(int id);
    }
}
