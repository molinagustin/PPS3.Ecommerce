namespace PPS3.Server.Repositories.RepTipoIVA
{
    public interface IRepTipoIVA
    {
        Task<IEnumerable<TipoIVA>> ObtenerTiposIVAs();
        Task<TipoIVA> ObtenerTipoIVA(int id);
    }
}
