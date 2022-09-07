namespace PPS3.Server.Repositories.RepCondicionIVA
{
    public class RepCondicionIVA : IRepCondicionIVA
    {
        private string _connectionString;

        public RepCondicionIVA(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<IEnumerable<CondicionIVA>> ObtenerCondicionesIVA()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM condiciones_iva
                        ORDER BY DescripcionCond
                        ";
            var result = await db.QueryAsync<CondicionIVA>(sql, new {});
            return result;
        }

        public async Task<CondicionIVA> ObtenerCondicionIVA(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM condiciones_iva
                        WHERE IdCondicion = @IdCondicion
                        ";
            var result = await db.QueryFirstOrDefaultAsync<CondicionIVA>(sql, new { IdCondicion = id });
            return result;
        }
    }
}
