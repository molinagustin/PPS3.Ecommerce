namespace PPS3.Server.Repositories.RepPuntoVenta
{
    public class RepPuntoVenta : IRepPuntoVenta
    {
        private string _connectionString;

        public RepPuntoVenta(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }
        
        public async Task<IEnumerable<PuntoVenta>> ObtenerPuntosVentas()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM puntos_venta
                        ";
            var result = await db.QueryAsync<PuntoVenta>(sql, new { });
            return result;
        }

        public async Task<PuntoVenta> ObtenerPuntoVenta(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM puntos_venta
                        WHERE IdPuntoVta = @IdPuntoVta
                        ";
            var result = await db.QueryFirstOrDefaultAsync<PuntoVenta>(sql, new { IdPuntoVta = id });
            return result;
        }
    }
}
