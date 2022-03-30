namespace PPS3.Server.Repositories.RepCondicionIVA
{
    public interface IRepCondicionIVA
    {
        Task<IEnumerable<CondicionIVA>> ObtenerCondicionesIVA();
        Task<CondicionIVA> ObtenerCondicionIVA(int id);
    }
}
