namespace PPS3.Server.Repositories.RepMovimientoCuentaCorriente
{
    public class RepMovimientoCuentaCorriente : IRepMovimientoCuentaCorriente
    {
        private string _connectionString;

        public RepMovimientoCuentaCorriente(SqlConfiguration connectionstring) => _connectionString = connectionstring.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> InsertarMovimiento(MovimientoCuentaCorriente movimientoCC)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO cuentas_corrientes_movimientos  
                                (
                                ImporteACuenta,
                                ImporteAbonado
                                )
                        VALUES  (
                                @ImporteACuenta,
                                @ImporteAbonado
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new 
                                {
                                movimientoCC.ImporteACuenta,
                                movimientoCC.ImporteAbonado
                                });
            if (result > 0)
                //Selecciono el ultimo ID ingresado para devolverlo y que se pueda ingresar en la cuenta corriente
                return await SeleccionarUltimoID();
            else
                return 0;
        }

        public async Task<MovimientoCuentaCorriente> ObtenerMovimiento(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cuentas_corrientes_movimientos
                        WHERE IdCCMov = @IdCCMov
                        ";
            var result = await db.QueryFirstOrDefaultAsync<MovimientoCuentaCorriente>(sql, new { IdCCMov = id });
            return result;
        }

        public async Task<IEnumerable<MovimientoCuentaCorriente>> ObtenerMovimientos()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cuentas_corrientes_movimientos
                        ";
            var result = await db.QueryAsync<MovimientoCuentaCorriente>(sql, new { });
            return result;
        }

        private async Task<int> SeleccionarUltimoID()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT MAX(IdCCMov)
                        FROM cuentas_corrientes_movimientos
                        ";
            var result = await db.ExecuteScalarAsync<int>(sql, new { });
            return result;
        }
    }
}