namespace PPS3.Client.Services.ServFormaPago
{
    public interface IServFormaPago
    {
        Task<IEnumerable<FormaPago>> ObtenerFormasPago();
        Task<FormaPago> ObtenerFormaPago(int id);
    }
}
