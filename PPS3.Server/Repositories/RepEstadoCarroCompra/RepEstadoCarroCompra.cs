namespace PPS3.Server.Repositories.RepEstadoCarroCompra
{
    public class RepEstadoCarroCompra : IRepEstadoCarroCompra
    {
        private string _connectionString;

        public RepEstadoCarroCompra(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<EstadoCarroCompra> ObtenerEstado(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM carros_compras_estados
                        WHERE IdEstado = @IdEstado
                        ";
            var result = await db.QueryFirstOrDefaultAsync<EstadoCarroCompra>(sql, new { IdEstado = id });
            return result;
        }

        public async Task<IEnumerable<EstadoCarroCompra>> ObtenerEstados()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM carros_compras_estados
                        ";
            var result = await db.QueryAsync<EstadoCarroCompra>(sql, new { });
            return result;
        }
    }
}