namespace PPS3.Server.Repositories.RepCuerpoPresupuesto
{
    public class RepCuerpoPresupuesto : IRepCuerpoPresupuesto
    {
        private string _connectionString;

        public RepCuerpoPresupuesto(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> InsertarNuevoCuerpo(CuerpoPresupuesto cuerpoPresupuesto)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO presupuestos_cuerpos(
                                                        IdEncab, 
                                                        Producto, 
                                                        Cantidad, 
                                                        PrecioUnit, 
                                                        Bonificacion, 
                                                        BonificacionTotal, 
                                                        SubTotal
                                                        )
                        VALUES  (
                                @IdEncab, 
                                @Producto, 
                                @Cantidad, 
                                @PrecioUnit, 
                                @Bonificacion, 
                                @BonificacionTotal, 
                                @SubTotal
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        cuerpoPresupuesto.IdEncab,
                                                        cuerpoPresupuesto.Producto,
                                                        cuerpoPresupuesto.Cantidad,
                                                        cuerpoPresupuesto.PrecioUnit,
                                                        cuerpoPresupuesto.Bonificacion,
                                                        cuerpoPresupuesto.BonificacionTotal,
                                                        cuerpoPresupuesto.SubTotal
                                                        });
            return result > 0;
        }

        public async Task<CuerpoPresupuesto> ObtenerCuerpoPresupuesto(int idCuerpo)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM presupuestos_cuerpos
                        WHERE IdCuerpoPres = @IdCuerpoPres
                        ";
            var result = await db.QueryFirstOrDefaultAsync<CuerpoPresupuesto>(sql, new  {
                                                                                        IdCuerpoPres = idCuerpo
                                                                                        });
            return result;
        }

        public async Task<IEnumerable<CuerpoPresupuesto>> ObtenerCuerposDePresupuesto(int numPresupuesto)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM presupuestos_cuerpos
                        WHERE IdEncab = @IdEncab
                        ";
            var result = await db.QueryAsync<CuerpoPresupuesto>(sql, new{
                                                                        IdEncab = numPresupuesto
                                                                        });
            return result;
        }
    }
}
