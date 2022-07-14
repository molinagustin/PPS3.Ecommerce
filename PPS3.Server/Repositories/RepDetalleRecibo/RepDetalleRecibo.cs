using PPS3.Shared.InternalModels;

namespace PPS3.Server.Repositories.RepDetalleRecibo
{
    public class RepDetalleRecibo : IRepDetalleRecibo
    {
        private string _connectionString;

        public RepDetalleRecibo(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> InsertarDetalle(DetalleRecibo detalleRec)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO recibos_detalles (
                                                     IdRecibo,
                                                     IdComprobante,
                                                     Importe
                                                     )
                        VALUES  (
                                @IdRecibo,
                                @IdComprobante,
                                @Importe
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        detalleRec.IdRecibo,
                                                        detalleRec.IdComprobante,
                                                        detalleRec.Importe
                                                        });
            return result > 0;
        }

        public async Task<IEnumerable<DetalleRecibo>> ObtenerDetallesPorComprobante(int idComprobante)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM recibos_detalles
                        WHERE IdComprobante = @IdComprobante
                        ";
            var result = await db.QueryAsync<DetalleRecibo>(sql, new { IdComprobante = idComprobante });
            return result;
        }

        public async Task<IEnumerable<DetalleRecibo>> ObtenerDetallesRecibo(int idRecibo)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM recibos_detalles
                        WHERE IdRecibo = @IdRecibo
                        ";
            var result = await db.QueryAsync<DetalleRecibo>(sql, new { IdRecibo = idRecibo });
            return result;
        }

        public async Task<IEnumerable<DetalleRecibo>> ObtenerDetallesRecibos()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM recibos_detalles
                        "
;
            var result = await db.QueryAsync<DetalleRecibo>(sql, new {  });
            return result;
        }
    }
}
