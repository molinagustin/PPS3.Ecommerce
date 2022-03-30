namespace PPS3.Server.Repositories.RepPrivilegio
{
    public interface IRepPrivilegio
    {
        Task<IEnumerable<Privilegio>> ObtenerPrivilegios();
        Task<Privilegio> ObtenerPrivilegio(int id);
    }
}
