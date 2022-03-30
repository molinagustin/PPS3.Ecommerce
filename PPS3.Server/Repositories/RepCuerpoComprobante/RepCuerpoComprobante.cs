namespace PPS3.Server.Repositories.RepCuerpoComprobante
{
    public class RepCuerpoComprobante : IRepCuerpoComprobante
    {
        private string _connectionString;

        public RepCuerpoComprobante(SqlConfiguration connectionString) => _connectionString = connectionString.ConnectionString;
        
        protected SqlConnection dbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<bool> InsertarCuerpoComp(CuerpoComprobante cuerpoComp)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO comprobantes_cuerpos(
                                                        IdEncab, 
                                                        ProductoCuerpo, 
                                                        CantidadProdC, 
                                                        PrecioUnitario, 
                                                        Bonificacion, 
                                                        Iva, 
                                                        BonificacionTotal, 
                                                        Total, 
                                                        UsuarioCrea
                                                        )
                        VALUES  (
                                @IdEncab, 
                                @ProductoCuerpo, 
                                @CantidadProdC, 
                                @PrecioUnitario, 
                                @Bonificacion, 
                                @Iva, 
                                @BonificacionTotal, 
                                @Total, 
                                @UsuarioCrea
                                )
                        ";
            var result = await db.ExecuteAsync(sql, new { 
                                                        cuerpoComp.IdEncab,
                                                        cuerpoComp.ProductoCuerpo,
                                                        cuerpoComp.CantidadProdC,
                                                        cuerpoComp.PrecioUnitario,
                                                        cuerpoComp.Bonificacion,
                                                        cuerpoComp.Iva,
                                                        cuerpoComp.BonificacionTotal,
                                                        cuerpoComp.Total,
                                                        UsuarioCrea = 1
                                                        });
            return result > 0;
        }

        public async Task<CuerpoComprobante> ObtenerCuerpoComp(int numCuerpo)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM comprobantes_cuerpos
                        WHERE IdCuerpo = @IdCuerpo
                        ";
            var result = await db.QueryFirstOrDefaultAsync<CuerpoComprobante>(sql, new { IdCuerpo = numCuerpo });
            return result;
        }

        public async Task<IEnumerable<CuerpoComprobante>> ObtenerCuerposDeComprobante(int numCabeceraComp)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                        FROM comprobantes_cuerpos
                        WHERE IdEncab = @IdEncab
                        ";
            var result = await db.QueryAsync<CuerpoComprobante>(sql, new { IdEncab = numCabeceraComp });
            return result;
        }
    }
}
