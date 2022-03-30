namespace PPS3.Server.Repositories.RepTarjeta
{
    public class RepTarjeta : IRepTarjeta
    {
        private string _connectionString;

        public RepTarjeta(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
         
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<Tarjeta> ObtenerTarjeta(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tarjetas
                        WHERE IdTarjeta = @IdTarjeta
                        "
;
            var result = await db.QueryFirstOrDefaultAsync<Tarjeta>(sql, new { IdTarjeta = id });
            return result;
        }

        public async Task<IEnumerable<Tarjeta>> ObtenerTarjetas()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tarjetas
                        ";
            var result = await db.QueryAsync<Tarjeta>(sql, new { });
            return result;
        }
    }
}
