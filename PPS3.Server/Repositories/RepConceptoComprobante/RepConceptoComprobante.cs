namespace PPS3.Server.Repositories.RepConceptoComprobante
{
    public class RepConceptoComprobante : IRepConceptoComprobante
    {
        private string _connectionString;

        public RepConceptoComprobante(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public async Task<ConceptoComprobante> ObtenerConceptoComprobante(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM comprobantes_conceptos
                        WHERE IdConcepto = @IdConcepto
                        ";
            var result = await db.QueryFirstOrDefaultAsync<ConceptoComprobante>(sql, new { IdConcepto = id });
            return result;
        }

        public async Task<IEnumerable<ConceptoComprobante>> ObtenerConceptosComprobantes()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM comprobantes_conceptos
                        ";
            var result = await db.QueryAsync<ConceptoComprobante>(sql, new { });
            return result;
        }
    }
}
