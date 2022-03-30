namespace PPS3.Server.Repositories.RepFormaPago
{
    public interface IRepFormaPago
    {
        Task<IEnumerable<FormaPago>> ObtenerFormasPago();
        Task<FormaPago> ObtenerFormaPago(int id);
    }
}
