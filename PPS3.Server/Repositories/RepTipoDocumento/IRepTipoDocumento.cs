namespace PPS3.Server.Repositories.RepTipoDocumento
{
    public interface IRepTipoDocumento
    {
        Task<IEnumerable<TipoDocumento>> ObtenerTiposDocumentos();
        Task<TipoDocumento> ObtenerTipoDocumento(int id);
    }
}
