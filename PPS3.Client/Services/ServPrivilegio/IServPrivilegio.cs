namespace PPS3.Client.Services.ServPrivilegio
{
    public interface IServPrivilegio
    {
        Task<IEnumerable<Privilegio>> ObtenerPrivilegios();
        Task<Privilegio> ObtenerPrivilegio(int id);
    }
}
