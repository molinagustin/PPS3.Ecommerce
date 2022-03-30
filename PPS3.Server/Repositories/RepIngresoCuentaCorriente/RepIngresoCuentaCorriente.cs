namespace PPS3.Server.Repositories.RepIngresoCuentaCorriente
{
    public class RepIngresoCuentaCorriente : IRepIngresoCuentaCorriente
    {
        private string _connectionString;

        public RepIngresoCuentaCorriente(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> InsertarIngresoCC(IngresoCuentaCorriente ingresoCC)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO cuentas_corrientes_ingresos
                                                    (
                                                    CuentaCorriente,
                                                    CompIngreso,
                                                    TipoIngreso,
                                                    MovimientoCC,
                                                    UsuarioCrea
                                                    )
                        VALUES  (
                                @CuentaCorriente,
                                @CompIngreso,
                                @TipoIngreso,
                                @MovimientoCC,
                                @UsuarioCrea
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new 
                                {
                                ingresoCC.CuentaCorriente,
                                ingresoCC.CompIngreso,
                                ingresoCC.TipoIngreso,
                                ingresoCC.MovimientoCC,
                                UsuarioCrea = 1
                                });
            return result > 0;
        }

        public async Task<IngresoCuentaCorriente> ObtenerIngresoCC(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cuentas_corrientes_ingresos
                        WHERE IdIngreso = @IdIngreso
                        ";
            var result = await db.QueryFirstOrDefaultAsync<IngresoCuentaCorriente>(sql, new { IdIngreso = id });
            return result;
        }

        public async Task<IEnumerable<IngresoCuentaCorriente>> ObtenerIngresosCC()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM cuentas_corrientes_ingresos
                        ";
            var result = await db.QueryAsync<IngresoCuentaCorriente>(sql, new {});
            return result;
        }
    }
}
