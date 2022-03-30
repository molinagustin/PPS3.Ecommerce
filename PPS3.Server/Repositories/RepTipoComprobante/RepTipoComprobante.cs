namespace PPS3.Server.Repositories.RepTipoComprobante
{
    public class RepTipoComprobante : IRepTipoComprobante
    {
        private string _connectionString;

        public RepTipoComprobante(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<TipoComprobante> ObtenerTipoComprobante(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_comprobantes
                        WHERE IdTipoC = @IdTipoC
                        ";
            var result = await db.QueryFirstOrDefaultAsync<TipoComprobante>(sql, new { IdTipoC = id });
            return result;
        }

        public async Task<IEnumerable<TipoComprobante>> ObtenerTiposComprobantes()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_comprobantes
                        ";
            var result = await db.QueryAsync<TipoComprobante>(sql, new { });
            return result;
        }
    }
}
