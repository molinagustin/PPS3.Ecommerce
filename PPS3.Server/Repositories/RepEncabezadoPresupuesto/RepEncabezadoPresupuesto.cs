namespace PPS3.Server.Repositories.RepEncabezadoPresupuesto
{
    public class RepEncabezadoPresupuesto : IRepEncabezadoPresupuesto
    {
        private string _connectionString;

        public RepEncabezadoPresupuesto(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> InsertarPresupuesto(EncabezadoPresupuesto encabezadoPresupuesto)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO presupuestos_encabezados(
                                                            Observaciones,
                                                            UsuarioCrea
                                                            )
                        VALUES  (
                                @Observaciones,
                                @UsuarioCrea
                                )                        
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        encabezadoPresupuesto.Observaciones,
                                                        UsuarioCrea = 1
                                                        });
            return result > 0;
        }

        public async Task<EncabezadoPresupuesto> ObtenerPresupuesto(int numPres)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM presupuestos_encabezados
                        WHERE NumPresu = @NumPresu
                        ";
            var result = await db.QueryFirstOrDefaultAsync<EncabezadoPresupuesto>(sql, new { NumPresu = numPres });
            return result;
        }

        public async Task<IEnumerable<EncabezadoPresupuesto>> ObtenerTodosPresupuestos()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM presupuestos_encabezados
                        ";
            var result = await db.QueryAsync<EncabezadoPresupuesto>(sql, new { });
            return result;
        }
    }
}
