namespace PPS3.Server.Repositories.RepCuerpoComprobante
{
    public interface IRepCuerpoComprobante
    {
        //Obtener los cuerpos de un numero de comprobante de cabecera
        Task<IEnumerable<CuerpoComprobante>> ObtenerCuerposDeComprobante(int numCabeceraComp);
        //Obtener un cuerpo de comprobante en particular
        Task<CuerpoComprobante> ObtenerCuerpoComp(int numCuerpo);
        Task<bool> InsertarCuerpoComp(CuerpoComprobante cuerpoComp);
    }
}
