namespace PPS3.Client.Services.ServTipoDocumento
{
    public interface IServTipoDocumento
    {
        Task<IEnumerable<TipoDocumento>> ObtenerTiposDocs();
        Task<TipoDocumento> ObtenerTipoDoc(int id);
    }
}
