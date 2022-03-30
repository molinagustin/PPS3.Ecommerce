namespace PPS3.Server.Repositories.RepGenero
{
    public class RepGenero : IRepGenero
    {
        private readonly string _connectionString;

        public RepGenero(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<Genero> ObtenerGenero(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM generos
                        WHERE IdGenero = @IdGenero
                        ";
            var result = await db.QueryFirstOrDefaultAsync<Genero>(sql, new { IdGenero = id });
            return result;
        }

        public async Task<IEnumerable<Genero>> ObtenerGeneros()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM generos
                        ";
            var result = await db.QueryAsync<Genero>(sql, new { });
            return result;
        }
    }
}
