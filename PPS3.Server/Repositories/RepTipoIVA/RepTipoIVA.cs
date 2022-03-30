namespace PPS3.Server.Repositories.RepTipoIVA
{
    public class RepTipoIVA : IRepTipoIVA
    {
        private string _connectionString;

        public RepTipoIVA(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<TipoIVA> ObtenerTipoIVA(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_iva
                        WHERE IdTipoIva = @IdTipoIva
                        ";
            var result = await db.QueryFirstOrDefaultAsync<TipoIVA>(sql, new { IdTipoIva = id });
            return result;
        }

        public async Task<IEnumerable<TipoIVA>> ObtenerTiposIVAs()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_iva
                        ";
            var result = await db.QueryAsync<TipoIVA>(sql, new { });
            return result;
        }
    }
}
