namespace PPS3.Server.Repositories.RepTipoVenta
{
    public class RepTipoVenta : IRepTipoVenta
    {
        private string _connectionString;

        public RepTipoVenta(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<IEnumerable<TipoVenta>> ObtenerTiposVentas()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_ventas
                        ";
            var result = await db.QueryAsync<TipoVenta>(sql, new { });
            return result;
        }

        public async Task<TipoVenta> ObtenerTipoVenta(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_ventas
                        WHERE IdTipoVta = @IdTipoVta
                        ";
            var result = await db.QueryFirstOrDefaultAsync<TipoVenta>(sql, new { IdTipoVta = id });
            return result;
        }
    }
}
