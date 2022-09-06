using PPS3.Shared.Models;

namespace PPS3.Server.Repositories.RepMovimientoCarroCompra
{
    public class RepMovCarro : IRepMovCarro
    {
        private string _connectionString = string.Empty;

        public RepMovCarro(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> CrearMovimiento(MovimientoCarroCompra movimiento)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO carros_compras_movimientos (
                                IdOrden,
                                IdEstado,
                                Usuario
                                )
                        VALUES (
                                @IdOrden,
                                @IdEstado,
                                @Usuario
                                )";

            //El metodo ExecuteAsync ejecuta una query sql y devuelve las filas afectadas
            var result = await db.ExecuteAsync(sql, new
            {
                movimiento.IdOrden,
                movimiento.IdEstado,
                movimiento.Usuario
            });

            return result > 0;
        }

        public async Task<IEnumerable<HistorialMovimientoCarro>> ObtenerHistorial(int idOrden)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT cce.Estado, u.NombreUs as Usuario, ccm.FechaUltModif
                        FROM carros_compras_movimientos as ccm
                        INNER JOIN carros_compras_estados as cce ON cce.IdEstado = ccm.IdEstado
                        INNER JOIN usuarios as u ON u.IdUsuarioAct = ccm.Usuario
                        WHERE ccm.IdOrden = @idOrden
                        ORDER BY ccm.FechaUltModif
                        ";

            //El metodo ExecuteAsync ejecuta una query sql y devuelve las filas afectadas
            var result = await db.QueryAsync<HistorialMovimientoCarro>(sql, new { idOrden });

            return result;
        }
    }
}
