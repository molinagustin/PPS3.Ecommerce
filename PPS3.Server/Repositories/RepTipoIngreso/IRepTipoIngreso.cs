namespace PPS3.Server.Repositories.RepTipoIngreso
{
    public interface IRepTipoIngreso
    {
        Task<IEnumerable<TipoIngreso>> ObtenerTiposIngresos();
        Task<TipoIngreso> ObtenerTipoIngreso(int id);
    }
}
