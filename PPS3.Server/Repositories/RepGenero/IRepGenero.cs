namespace PPS3.Server.Repositories.RepGenero
{
    public interface IRepGenero
    {
        Task<IEnumerable<Genero>> ObtenerGeneros();
        Task<Genero> ObtenerGenero(int id);
    }
}
