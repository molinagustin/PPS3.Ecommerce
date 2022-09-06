namespace PPS3.Server.Repositories.RepFormaPago
{
    public class RepFormaPago : IRepFormaPago
    {
        private string _connectionString;

        public RepFormaPago(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<FormaPago> ObtenerFormaPago(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM formas_pago
                        WHERE IdFormaP = @IdFormaP
                        ";
            var result = await db.QueryFirstOrDefaultAsync<FormaPago>(sql, new { IdFormaP = id });
            return result;
        }

        public async Task<IEnumerable<FormaPago>> ObtenerFormasPago()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM formas_pago
                        WHERE Activo = 1
                        ";
            var result = await db.QueryAsync<FormaPago>(sql, new {});
            return result;
        }
    }
}
