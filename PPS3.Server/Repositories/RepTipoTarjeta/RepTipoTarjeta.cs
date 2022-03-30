namespace PPS3.Server.Repositories.RepTipoTarjeta
{
    public class RepTipoTarjeta : IRepTipoTarjeta
    {
        private string _connectionString;

        public RepTipoTarjeta(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<TipoTarjeta>> ObtenerTiposTarjetas()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tarjetas_tipos
                        ";
            var result = await db.QueryAsync<TipoTarjeta>(sql, new { });
            return result;
        }

        public async Task<TipoTarjeta> ObtenerTipoTarjeta(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tarjetas_tipos
                        WHERE IdTipoTarj = @IdTipoTarj
                        ";
            var result = await db.QueryFirstOrDefaultAsync<TipoTarjeta>(sql, new { IdTipoTarj = id });
            return result;
        }
    }
}
