namespace PPS3.Server.Repositories.RepPrivilegio
{
    public class RepPrivilegio : IRepPrivilegio
    {
        private string _connectionString;

        public RepPrivilegio(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<Privilegio> ObtenerPrivilegio(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM usuarios_privilegios
                        WHERE IdPrivi = @IdPrivi
                        ";
            var result = await db.QueryFirstOrDefaultAsync<Privilegio>(sql, new { IdPrivi = id });
            return result;
        }

        public async Task<IEnumerable<Privilegio>> ObtenerPrivilegios()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM usuarios_privilegios
                        ";
            var result = await db.QueryAsync<Privilegio>(sql, new { });
            return result;
        }
    }
}
