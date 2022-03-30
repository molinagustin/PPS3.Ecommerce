using PPS3.Shared.Models;

namespace PPS3.Server.Repositories.RepTipoIngreso
{
    public class RepTipoIngreso : IRepTipoIngreso
    {
        private string _connectionString;

        public RepTipoIngreso(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<TipoIngreso> ObtenerTipoIngreso(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_ingresos
                        WHERE IdTipoIngreso = @IdTipoIngreso
                        "
;
            var result = await db.QueryFirstOrDefaultAsync<TipoIngreso>(sql, new { IdTipoIngreso = id });
            return result;
        }

        public async Task<IEnumerable<TipoIngreso>> ObtenerTiposIngresos()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM tipos_ingresos
                        ";
            var result = await db.QueryAsync<TipoIngreso>(sql, new { });
            return result;
        }
    }
}