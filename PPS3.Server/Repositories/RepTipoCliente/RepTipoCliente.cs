namespace PPS3.Server.Repositories.RepTipoCliente
{
    public class RepTipoCliente : IRepTipoCliente
    {
        private string _connectionString;

        public RepTipoCliente(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<TipoCliente> ObtenerTipoCliente(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_clientes
                        WHERE IdTipoCl = @IdTipoCl
                        ";
            var result = await db.QueryFirstOrDefaultAsync<TipoCliente>(sql, new { IdTipoCl = id });
            return result;
        }

        public async Task<IEnumerable<TipoCliente>> ObtenerTiposClientes()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_clientes
                        ";
            var result = await db.QueryAsync<TipoCliente>(sql, new {});
            return result;
        }
    }
}
