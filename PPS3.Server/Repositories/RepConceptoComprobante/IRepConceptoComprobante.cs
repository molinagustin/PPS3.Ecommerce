namespace PPS3.Server.Repositories.RepConceptoComprobante
{
    public interface IRepConceptoComprobante
    {
        Task<IEnumerable<ConceptoComprobante>> ObtenerConceptosComprobantes();
        Task<ConceptoComprobante> ObtenerConceptoComprobante(int id);
    }
}
