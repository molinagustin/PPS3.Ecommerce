namespace PPS3.Server.Repositories.RepProvincia
{
    public class RepProvincia : IRepProvincia
    {
        private string _connectionString;

        public RepProvincia(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<Provincia> ObtenerProvincia(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM provincias
                        WHERE IdProvincia = @IdProvincia
                        ";
            var result = await db.QueryFirstOrDefaultAsync<Provincia>(sql, new { IdProvincia = id });
            return result;
        }

        public async Task<Provincia> ObtenerProvincia(string nombreProvincia)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM provincias
                        WHERE NombreProv=@NombreProv
                        ";
            var result = await db.QueryFirstOrDefaultAsync<Provincia>(sql, new { NombreProv = nombreProvincia });
            return result;
        }

        public async Task<IEnumerable<Provincia>> ObtenerProvincias()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM provincias
                        ";
            var result = await db.QueryAsync<Provincia>(sql, new { });
            return result;
        }
    }
}