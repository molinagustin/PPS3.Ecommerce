namespace PPS3.Server.Repositories.RepTipoDocumento
{
    public class RepTipoDocumento : IRepTipoDocumento
    {
        private string _connectionString;

        public RepTipoDocumento(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<TipoDocumento> ObtenerTipoDocumento(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_documentos
                        WHERE IdTipoDoc = @IdTipoDoc
                        ";
            var result = await db.QueryFirstOrDefaultAsync<TipoDocumento>(sql, new { IdTipoDoc = id });
            return result;
        }

        public async Task<IEnumerable<TipoDocumento>> ObtenerTiposDocumentos()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_documentos
                        ";
            var result = await db.QueryAsync<TipoDocumento>(sql, new { });
            return result;
        }
    }
}
